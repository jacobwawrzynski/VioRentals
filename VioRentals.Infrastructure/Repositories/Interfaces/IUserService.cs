using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.DTOs;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IUserService
    {
        // Login
        //public Task<AuthenticateUserDto> AuthenticateAsync(LoginDto loginDto);
        public Task<IEnumerable<UserEntity>> FindAllAsync();

        // Register (Add)
        //public Task<bool> RegisterAsync(RegisterDto registerDto);

        // Add
        public Task<bool> SaveUserAsync(UserEntity user); 
        public Task<UserEntity?> FindByIdAsync(int id);
        public Task<UserEntity?> FindByEmailAsync(string email);
        public Task<bool> UpdateUserAsync(UserEntity user);
        public Task<bool> DeleteUserAsync(UserEntity user);
    }
}
