using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quarterly_Sales.Models
{
    public class SalesContext : DbContext
    {

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    DOB = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId = 0
                }
                );
        }
    }
}
