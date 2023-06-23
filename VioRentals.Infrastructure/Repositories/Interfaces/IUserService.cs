using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IUserService
    {
        public Task<bool> SaveUserAsync(UserEntity user);
        public Task<UserEntity?> FindByIdAsync(int id);
        public Task<UserEntity?> FindByEmailAsync(string email);
    }
}
