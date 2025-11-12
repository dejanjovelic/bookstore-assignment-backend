using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleEditor = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var editor1 = await userManager.FindByNameAsync("john1998");
            if (editor1 == null)
            {
                editor1 = new ApplicationUser
                {
                    UserName = "john1998",
                    Email = "john78@gmail.com",
                    Name = "John",
                    Surname = "Smith",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor1, "JohnS$78");
            }
            if (!await userManager.IsInRoleAsync(editor1, "Editor"))
            {
                await userManager.AddToRoleAsync(editor1, "Editor");
            }

            var editor2 = await userManager.FindByNameAsync("ned2000");
            if (editor2 == null)
            {
                editor2 = new ApplicationUser
                {
                    UserName = "ned2000",
                    Email = "ned00@gmail.com",
                    Name = "Ned",
                    Surname = "Anderson",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor2, "NedAn@00");
            }

            if (!await userManager.IsInRoleAsync(editor2, "Editor"))
            {
                await userManager.AddToRoleAsync(editor2, "Editor");
            }

            var editor3 = await userManager.FindByNameAsync("mich85");
            if (editor3 == null)
            {
                editor3 = new ApplicationUser
                {
                    UserName = "mich85",
                    Email = "mich85@gmail.com",
                    Name = "Michael",
                    Surname = "Mitchell",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor3, "Mich@985");
            }
            if (!await userManager.IsInRoleAsync(editor3, "Editor"))
            {
                await userManager.AddToRoleAsync(editor3, "Editor");
            }
        }
    }
}
