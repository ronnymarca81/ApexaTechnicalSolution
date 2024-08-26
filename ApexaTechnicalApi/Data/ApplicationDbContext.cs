using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexaTechnicalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexaTechnicalApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Advisor> Advisors { get; set; }
        public DbSet<User> Users { get; set; }

    }
}