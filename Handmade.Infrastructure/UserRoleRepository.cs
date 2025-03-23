using Handmade.Application.Contracts;
using Handmade.Context;
using Handmade.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Handmade.Application.UserRoleRepository;

namespace Handmade.Application
{
    public class UserRoleRepository(HandmadeContext context) : IUserRoleRepository
    {
        public Task<UserRole> DeleteAsync(UserRole userRole) => Task.FromResult(context.UserRoles.Remove(userRole).Entity);

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
        public Task<IQueryable<UserRole>> GetAllAsync() => Task.FromResult((IQueryable<UserRole>)context.UserRoles);
    }
}
