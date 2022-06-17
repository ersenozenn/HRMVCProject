using HRMVCProjectEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Data
{
    public class HRMVCProjectDbContext : IdentityDbContext<User,UserRole,int>
    {
        public HRMVCProjectDbContext(DbContextOptions<HRMVCProjectDbContext> options) : base(options)
        {
            Database.SetCommandTimeout((int)TimeSpan.FromMinutes(90).TotalSeconds);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }     
        public DbSet<AdvancePayment> AdvancePayment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CostType> CostTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(a => a.Identity).IsRequired().HasMaxLength(13);
            modelBuilder.Entity<Employee>().Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(a => a.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(a => a.DateStarted).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.BirthDate).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.Email).IsRequired().HasMaxLength(50);
            //e mail unique olacak.

            modelBuilder.Entity<Employee>().Property(a => a.Telephone).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Employee>().Ignore(a => a.UserPhoto);
            modelBuilder.Entity<Cost>().Ignore(a => a.CostFile);
           
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User",NormalizedName="USER" });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Name = "User",NormalizedName="USER",Id=1 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Yemek",Id=1 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Seyehat",Id=2 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Ulaşım",Id=3 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Konaklama",Id=4 });
            base.OnModelCreating(modelBuilder);
            //RoleManager = new RoleManager<IdentityRole>(
            //       new RoleStore<IdentityRole>(new HRMVCProjectDbContext()));
            //var roleresult = RoleManager.Create(new IdentityRole("User"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=***;Initial Catalog=HRProjectDb;Persist Security Info=False;User ID=***;Password=***;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");
            }
        }
    }
}
