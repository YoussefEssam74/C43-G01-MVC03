using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Demo.DataAccess.Models.DepartmentModel;
using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Demo.DataAccess.Models.IdentityModel;
namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) //PC
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        //{

        //}



        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
      




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer( "connectionString");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly)
            base.OnModelCreating(modelBuilder);
        }
    }
}
