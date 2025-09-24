using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.IdentityDb;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Common.Models;

public class UserDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ArgumentNullException.ThrowIfNull(builder);

        // Configure ApplicationUser table name if needed
        builder?.Entity<ApplicationUser>().ToTable("ApplicationUsers");

        // Seed roles
        builder?.Entity<IdentityRole<Guid>>().ToTable("Roles").HasData(
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Owner",
                NormalizedName = "OWNER",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Editor",
                NormalizedName = "EDITOR",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            });
    }
}