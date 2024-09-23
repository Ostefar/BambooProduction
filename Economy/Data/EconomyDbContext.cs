using Economy.Models;
using Microsoft.EntityFrameworkCore;

namespace Economy.Data
{
    public class EconomyDbContext : DbContext
    {
        public EconomyDbContext(DbContextOptions<EconomyDbContext> options)
        : base(options)
        {
        }

        //not implemented yet
        //public DbSet<CustomerEco> CustomerEcos { get; set; }

        //public DbSet<FixedEco> FixedEcos { get; set; }

        public DbSet<EmployeeEco> EmployeeEcos { get; set; }

        public DbSet<SickLeave> SickLeaves { get; set; }

        public DbSet<Vacation> Vacations { get; set; }

        public DbSet<ProjectEco> ProjectEcos { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Hour> Hours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEco>()
                .HasMany(p => p.Hours)
                .WithOne(h => h.ProjectEco)
                .HasForeignKey(h => h.ProjectEcoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEco>()
                .HasMany(p => p.Material)
                .WithOne(m => m.ProjectEco)
                .HasForeignKey(m => m.ProjectEcoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeEco>()
               .HasMany(p => p.SickLeaves)
               .WithOne(h => h.EmployeeEco)
               .HasForeignKey(h => h.EmployeeEcoId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeEco>()
                .HasMany(p => p.VacationDays)
                .WithOne(m => m.EmployeeEco)
                .HasForeignKey(m => m.EmployeeEcoId)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
