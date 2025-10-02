namespace ToDoListApp.Database;

using Microsoft.EntityFrameworkCore;
using ToDoList.Common.Models;

public class ToDoListDbContext : DbContext
{
    public ToDoListDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<ToDoList> ToDoLists => this.Set<ToDoList>();

    public virtual DbSet<TaskItem> TaskItems => this.Set<TaskItem>();

    public virtual DbSet<ToDoListUser> ToDoListUsers => this.Set<ToDoListUser>();

    protected override void OnModelCreating(ModelBuilder? modelBuilder)
    {
        if (modelBuilder == null)
            return;

        // ToDoList configuration
        modelBuilder.Entity<ToDoList>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Description).HasMaxLength(500);
            entity.Property(t => t.Status).IsRequired();

            // One ToDoList has many TaskItems
            entity.HasMany(t => t.Tasks)
                .WithOne()
                .HasForeignKey("ToDoListId")
                .OnDelete(DeleteBehavior.Cascade);

            // One ToDoList has many UserRoles
            entity.HasMany(t => t.UserRoles)
                .WithOne(ur => ur.ToDoList)
                .HasForeignKey(ur => ur.ToDoListId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // TaskItem configuration
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(ti => ti.Id);
            entity.Property(ti => ti.Title).IsRequired().HasMaxLength(100);
            entity.Property(ti => ti.Description).HasMaxLength(500);
            entity.Property(ti => ti.DateCreated).IsRequired();
            entity.Property(ti => ti.DueDate).IsRequired();
            entity.Property(ti => ti.AssignedUserId).IsRequired();
            entity.Property(ti => ti.Status).IsRequired();

            // Add ToDoListId as foreign key
            entity.Property<Guid>("ToDoListId").IsRequired();
        });

        // ToDoListUser configuration (join table)
        modelBuilder.Entity<ToDoListUser>(entity =>
        {
            entity.HasKey(ur => new { ur.ToDoListId, ur.UserId }); // composite key

            entity.Property(ur => ur.Role)
                .IsRequired()
                .HasConversion<string>(); // store enum as string
        });
    }
}