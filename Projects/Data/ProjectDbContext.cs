﻿using Microsoft.EntityFrameworkCore;
using Projects.Models;
using System.Collections.Generic;

namespace Projects.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

    }
}
