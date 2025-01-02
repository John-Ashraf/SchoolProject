using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;

namespace School.infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 2)
            {
                var defaultuser = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "JohnAshraf",
                    Country = "Egypt",
                    PhoneNumber = "2121212",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                _ = await _userManager.CreateAsync(defaultuser, "M123_m");
                _ = await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}

