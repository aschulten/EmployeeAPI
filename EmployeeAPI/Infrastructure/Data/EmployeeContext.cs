using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Infrastructure.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.LastSalary)
                    .HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.LastName)
                      .HasDatabaseName("IX_Employee_LastName");
                entity.HasIndex(e => e.Id)
                      .IsUnique()
                      .HasDatabaseName("IX_Employee_EmployeeId");
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}
