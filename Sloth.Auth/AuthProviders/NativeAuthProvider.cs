using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sloth.Auth.Models;
using Sloth.Auth.TokenProvider;
using Sloth.DB.Models;
using Sloth.DB.Repositories;

namespace Sloth.Auth.AuthProviders
{
    public class NativeAuthProvider : IAuthProvider
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly SlothAuthenticationOptions _authenticationOptions;

        public NativeAuthProvider(IMapper mapper,
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IOptions<SlothAuthenticationOptions> authenticationOptions) {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authenticationOptions = authenticationOptions.Value;
        }
        public async Task<AuthResponse> Login(IdentityModel model)
        {
            var user = await _userRepository.GetUserByLoginAsync(model.Login);
            if (user == null) {
                throw new NotImplementedException();
            }
            if (_passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Failed) {
                throw new NotImplementedException();
            }
            //TODO tokenProvider
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Guid.Empty.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _authenticationOptions.ApiName,
                SigningCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new AuthResponse() { Login = model.Login, AccessToken = tokenString };
        }

        public async Task<Guid> Logon(RegisterModel model)
        {
            if (await _userRepository.AnyByLoginAsync(model.Login))
            {
                throw new Exception("Username \"" + model.Login + "\" is already taken");
            }
                
            var user = _mapper.Map<User>(model);
            user.Password = _passwordHasher.HashPassword(null, model.Password);
            return await _userRepository.AddAsync(user);
        }

        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken) {
            return await Task.Run(() =>
            {
                SecurityToken validatedToken;
                var key = Encoding.ASCII.GetBytes(Guid.Empty.ToString());

                TokenValidationParameters validationParameters = new TokenValidationParameters();
                validationParameters.IssuerSigningKey = _authenticationOptions.GetSymmetricSecurityKey();
                validationParameters.ValidateAudience = false;
                validationParameters.ValidIssuer = _authenticationOptions.ApiName;
                validationParameters.ClockSkew = TimeSpan.FromSeconds(5);
                return new JwtSecurityTokenHandler().ValidateToken(authenticationToken, validationParameters, out validatedToken);
            });
        }

        public async Task<CurrentUser> GetCurrentUser(string name)
        {
            var user = await _userRepository.GetUserByLoginAsync(name);
            return _mapper.Map<CurrentUser>(user); ; 
        }
    }
}
