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

    protected override void OnModelCreating(ModelBuilder? modelBuilder)
    {
        // ToDoList configuration
        if (modelBuilder == null)
        {
            return;
        }

        modelBuilder.Entity<ToDoList>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Description).HasMaxLength(500);
            entity.Property(t => t.Status).IsRequired();

            // Ignore UserRoles dictionary for now (requires join table for full support)
            entity.Ignore(t => t.UserRoles);

            // One ToDoList has many TaskItems
            entity.HasMany(t => t.Tasks)
                .WithOne()
                .HasForeignKey("ToDoListId")
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
    }
}