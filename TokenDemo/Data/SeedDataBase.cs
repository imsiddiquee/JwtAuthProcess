using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenDemo.Data
{
    public class SeedDataBase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    
                    //Id="2",
                    //SecurityStamp = Guid.NewGuid().ToString(),                    
                    UserName = "R",
                    Email = "a@yahoo.com"

                    //EmailConfirmed = true,
                    //NormalizedUserName = "a@yahoo.com",                   
                    //NormalizedEmail = "a@yahoo.com",                    
                    //PhoneNumber = "Phone number omitted",
                    //PhoneNumberConfirmed = true,
                    //TwoFactorEnabled = false,
                    //LockoutEnabled=true,
                    //AccessFailedCount=1
                };

                //IdentityResult result = userManager.CreateAsync(user, "PasswordHere").Result;

                var  ir= userManager.CreateAsync(user, "Abc123!").GetAwaiter();

                if (ir.IsCompleted)
                {
                   // logger.LogDebug($"Created default user `{email}` successfully");
                }
                else
                {
                    //var exception = new ApplicationException($"Default user `{email}` cannot be created");
                    //logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                    //throw exception;
                }

               // var createdUser = await um.FindByEmailAsync(email);

                context.SaveChanges();
            }
        }

        //public static void SeedUsers(UserManager<IdentityUser> userManager)
        //{
        //    if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
        //    {
        //        IdentityUser user = new IdentityUser
        //        {
        //            UserName = "abc@xyz.com",
        //            Email = "abc@xyz.com"
        //        };

        //        IdentityResult result = userManager.CreateAsync(user, "PasswordHere").Result;

        //        if (result.Succeeded)
        //        {
        //            userManager.AddToRoleAsync(user, "Admin").Wait();
        //        }
        //    }
        //}
    }
}
