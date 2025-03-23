using Handmade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface IUserRoleRepository
    {
        public Task<UserRole> DeleteAsync(UserRole userRole);
        public Task<int> SaveChangesAsync();
        public Task<IQueryable<UserRole>> GetAllAsync();
    }
}
