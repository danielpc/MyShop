using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Security.Hashing;
using Supermarket.API.Domain.Service;
using Supermarket.API.Domain.Service.Communication;
using Supermarket.API.Resources.User;


namespace Supermarket.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

      

//        public async Task<GenericResponse<User>> SaveAsync(User user)
//        {
//            try
//            {
//                await _userRepository.AddAsync(user);
//                await _unitOfWork.CompleteAsync();
//                return new GenericResponse<User>(user);
//            }
//            catch (Exception ex)
//            {
//                // TODO Do some logging stuff
//                return new GenericResponse<User>($"An error occurred saving the user: {ex.Message}");
//            }
//        }

        public async Task<GenericResponse<User>> FindByIdAsync(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
       
            if(user == null)
                return new GenericResponse<User>("User not found");
            
            return new GenericResponse<User>(user);
        }

        public async Task<GenericResponse<User>> CreateUserAsync(User user, params ERole[] userRoles)
        {
            var existingUser = await _userRepository.FindByEmailAsync(user.Email);
            
            if (existingUser != null)
                return new GenericResponse<User>("Email already in use.");

            user.Password = _passwordHasher.HashPassword(user.Password);

            try
            {
                await _userRepository.AddAsync(user, userRoles);
                await _unitOfWork.CompleteAsync();
                return new GenericResponse<User>(user);
            }
            catch (Exception ex)
            {
                return  new GenericResponse<User>($"An error occurred saving the user: {ex.Message}");
            }
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userRepository.FindByEmailAsync(email);
        }
    }
}