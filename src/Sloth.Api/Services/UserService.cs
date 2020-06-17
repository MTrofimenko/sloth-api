using AutoMapper;
using Sloth.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sloth.Api.Models;
using System.Linq;

namespace Sloth.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetCurrentUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetUsersByNameAsync(string namePart)
        {
            var users = (await _userRepository.GetUsersByNameAsync(namePart)).Select(x => _mapper.Map<UserModel>(x));
            return users;
        }
    }
}