using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sloth.Auth.Models;
using Sloth.Auth.TokenProvider;
using Sloth.DB.Models;
using Sloth.DB.Repositories;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sloth.Auth.AuthProviders
{
    public class NativeAuthProvider : IAuthProvider
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenProvider _tokenProvider;
        private readonly ISessionRefreshTokenRepository _sessionRefreshTokenRepository;

        public NativeAuthProvider(IMapper mapper,
            IUserRepository userRepository,
            ISessionRefreshTokenRepository sessionRefreshTokenRepository,
            IPasswordHasher<User> passwordHasher,
            ITokenProvider tokenProvider) {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
            _sessionRefreshTokenRepository = sessionRefreshTokenRepository;
        }
        public async Task<AuthResponse> LoginAsync(IdentityModel model)
        {
            var user = await _userRepository.GetUserByLoginAsync(model.Login);
            if (user == null) {
                throw new UnauthorizedAccessException("Username \"" + model.Login + "\" not found");
            }
            if (_passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Failed) {
                throw new UnauthorizedAccessException();
            }
            return GetAuthResponse(user, await CreateRefreshTokenAsync(user));
        }
        public async Task<Guid> LogonAsync(RegisterModel model)
        {
            if (await _userRepository.AnyByLoginAsync(model.Login))
            {
                throw new Exception("Username \"" + model.Login + "\" is already taken");
            }
                
            var user = _mapper.Map<User>(model);
            user.Password = _passwordHasher.HashPassword(null, model.Password);
            return await _userRepository.AddAsync(user);
        }

        public async Task LogoutAsync(Guid userId)
        {
            await DeactivateRefreshTokenByUserIdAsync(userId);
        }

        public async Task<AuthResponse> RefreshAsync(RefreshModel model)
        {
            var user = await GetUserByAccessTokenAsync(model.AccessToken);
            if (user == null)
            {
                throw new SecurityTokenException("Invalid token!");
            }
            var refreshToken = await _sessionRefreshTokenRepository.GetSessionByTokenAsync(model.RefreshToken);
            if (refreshToken == null || refreshToken.UserId != user.Id) {
                throw new SecurityTokenException("Invalid token!");
            } else if (!refreshToken.IsActive || DateTime.UtcNow > refreshToken.ExpiredTime) {
                throw new SecurityTokenException("Expired token!");
            }            
            return GetAuthResponse(user, await UpdateRefreshToken(refreshToken));
        }

        private AuthResponse GetAuthResponse(User user, SessionRefreshToken refreshToken)
        {
            return new AuthResponse()
            {
                Login = user.Login,
                AccessToken = _tokenProvider.CreateToken(user),
                Expires = _tokenProvider.TokenExpires,
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            var claims = _tokenProvider.IntrospectToken(accessToken, out _);
            var sid = claims?.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(sid))
            {
                throw new SecurityTokenException($"Missing claim: {ClaimTypes.Sid}!");
            }
            return await _userRepository.GetUserByIdAsync(new Guid(sid));
        }

        private async Task<SessionRefreshToken> CreateRefreshTokenAsync(User user)
        {
            var token = new SessionRefreshToken() 
            {
                Token = _tokenProvider.CreateRefreshToken(),
                ExpiredTime = _tokenProvider.RefreshTokenExpires,
                IsActive = true,
                UserId = user.Id
            };
            await _sessionRefreshTokenRepository.AddAsync(token);
            return token;
        }
        private async Task<SessionRefreshToken> UpdateRefreshToken(SessionRefreshToken refreshToken)
        {
            refreshToken.Token = _tokenProvider.CreateRefreshToken();
            refreshToken.ExpiredTime = _tokenProvider.RefreshTokenExpires;
            return await _sessionRefreshTokenRepository.UpdateAsync(refreshToken);
        }
        private async Task DeactivateRefreshTokenByUserIdAsync(Guid userId)
        {
            var activeTokens = await _sessionRefreshTokenRepository.GetAllActiveTokenByUserIdAsync(userId);
            foreach (var refreshToken in activeTokens) {
                refreshToken.IsActive = false;
            }
            await _sessionRefreshTokenRepository.UpdateRangeAsync(activeTokens);
        }
    }
}
