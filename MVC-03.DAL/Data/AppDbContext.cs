using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=MVC-5;Trusted_Connection=True;MultipleActionResultSets=True;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
               base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>()
                        .ToTable("Roles");
            modelBuilder.Entity<IdentityUser>()
                        .ToTable("Users");
                       
        }

        public DbSet<Employee> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
     //   public IdentityUser<int> Users { get; set; }
       // public IdentityRole Roles { get; set; }
    }
}
