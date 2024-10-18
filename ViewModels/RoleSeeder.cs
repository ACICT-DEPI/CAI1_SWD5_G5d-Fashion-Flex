﻿using Microsoft.AspNetCore.Identity;

public static class RoleSeeder
{
	public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
	{
		var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		var roles = new List<string> { "Admin", "Customer" };

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}
}