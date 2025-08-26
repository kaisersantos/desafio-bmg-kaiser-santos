using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Seeds;

public static class SeedHelper
{
    public static void SeedAdminUser(BmgDbContext context)
    {
        var email = "admin@bmg.com";
        if (context.Users.Any(u => u.Email == email))
            return;

        var password = "Admin@123";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var adminUser = new UserEntity
        {
            Name = "Administrador",
            Email = email,
            PasswordHash = passwordHash,
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(adminUser);
        context.SaveChanges();
    }
}
