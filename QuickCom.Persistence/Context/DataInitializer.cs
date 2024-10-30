using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickCom.Domain.AppSettingModels;
using QuickCom.Domain.Entities;
using QuickCom.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCom.Persistence.Context
{
    public class DataInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public DataInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _configuration = configuration;
        }


        public async Task SeedData()
        {
            await _appDbContext.Database.MigrateAsync();
            await CreateRoles();
            await CreateSuperAdmin();
        }

        private async Task CreateRoles()
        {
            var roles = Enum.GetNames(typeof(RoleType));
            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) != null)
                    continue;

                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role
                });

            }
        }

        private async Task CreateSuperAdmin()
        {
            var superAdmin = _configuration.GetSection("SuperAdmin").Get<SuperAdmin>();
            if (superAdmin == null) return;

            var existSuperAdmin = await _userManager.FindByNameAsync(superAdmin.Username);
            if (existSuperAdmin != null) return;

            var adminUser = new AppUser()
            {
                Fullname = superAdmin.Fullname,
                Email = superAdmin.Email,
                UserName = superAdmin.Username
            };
            var result = await _userManager.CreateAsync(adminUser, superAdmin.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User hasn't created");
            }
            result = await _userManager.AddToRoleAsync(adminUser, RoleType.SuperAdmin.ToString());
            result = await _userManager.AddToRoleAsync(adminUser, RoleType.Admin.ToString());
            if (!result.Succeeded)
            {
                throw new Exception("User can't assigned");
            }
        }
    }
}
