using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentCalculation.Domain.Entities;
using PaymentCalculationApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculation.Infrastructure.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //connectionstring = "Server=.;Database=TESTDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MonthlySalary>()
          .HasOne(ms => ms.Employee)
          .WithMany(e => e.MonthlySalaries)
          .HasForeignKey(ms => ms.EmployeeId);


            base.OnModelCreating(builder);
        }

       
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MonthlySalary> MonthlySalary { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
