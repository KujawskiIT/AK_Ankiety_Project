using AKAnkietyProject.Models;
using Microsoft.AspNetCore.Identity;

namespace AKAnkietyProject.Data
{
    // Klasa pomocnicza
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Ankieter", "Respondent" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            await CreateUserAsync(userManager, "ankieter@test.pl", "Haslo123!", "Ankieter");

            await CreateUserAsync(userManager, "respondent@test.pl", "Haslo123!", "Respondent");
        }

        private static async Task CreateUserAsync(
            UserManager<ApplicationUser> userManager,
            string email,
            string password,
            string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
