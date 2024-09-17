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

    }
}
