using EmployeesTransportManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesTransportManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configurations can be placed here
            // no cascade delete for Employee, Coordinator, Project
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany(u => u.Employees)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Coordinator>()
                .HasOne(e => e.User)
                .WithMany(u => u.Coordinators)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
