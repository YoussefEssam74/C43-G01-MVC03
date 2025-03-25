using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) //PC
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        //{

        //}



        public DbSet<Department> Departments { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer( "connectionString");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly)
        }
    }
}
