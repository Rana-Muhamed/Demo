using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAppDbcontext : IdentityDbContext<ApplicationUser>

    {
        public MVCAppDbcontext(DbContextOptions<MVCAppDbcontext>options):base(options) 
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
        //    => optionsBuilder.UseSqlServer("Server =.; DataBase = MVCAppDB; Trusted_Connection= ture; ");
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get;set; }

        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityRole> Roles { get; set; }


    }
}
