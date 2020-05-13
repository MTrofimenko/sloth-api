using AutoMapper;
using Sloth.Auth.Models;
using Sloth.DB.Models;
using Sloth.DB.Repositories;
using System;
using System.Threading.Tasks;

namespace Sloth.Api.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, 
            IUserRepository userRepository) {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<CurrentUser> GetCurrentUserAsync(Guid id) {
            User user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<CurrentUser>(user);
        }
    }
}
