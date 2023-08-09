using System;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence.Data;

namespace Persistence.Seed
{
	public class Seed
	{
        public static async Task SeedData(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
             var roles = Enum.GetValues(typeof(Roles))
                            .Cast<Roles>()
                            .ToList();

            foreach (var item in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(item.ToString());
                if (!roleExist)
                {
                    var role = new ApplicationRole
                    {
                        Name = item.ToString(),
                        NormalizedName = item.ToString().ToUpper()
                    };
                    await roleManager.CreateAsync(role);
                }
            }
            await context.SaveChangesAsync();
        }
	}
}

