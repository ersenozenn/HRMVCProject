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
    public class HRMVCProjectDbContext : IdentityDbContext<User, UserRole, int>
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
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(a => a.Identity).IsRequired().HasMaxLength(13);
            modelBuilder.Entity<Employee>().Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(a => a.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(a => a.DateStarted).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.BirthDate).IsRequired();
            modelBuilder.Entity<Employee>().Property(a => a.Email).IsRequired().HasMaxLength(50);
            //e mail unique olacak.

            // modelBuilder.Entity<Employee>().Property(a => a.Telephone).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Employee>().Ignore(a => a.UserPhoto);
            modelBuilder.Entity<Cost>().Ignore(a => a.CostFile);
            modelBuilder.Entity<Package>().Ignore(a => a.PackagePhoto);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Name = "User", NormalizedName = "USER", Id = 1 });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Name = "Manager", NormalizedName = "MANAGER", Id = 2 });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Name = "Admin", NormalizedName = "ADMIN", Id = 3 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Yemek", Id = 1 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Seyehat", Id = 2 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Ulaşım", Id = 3 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Konaklama", Id = 4 });
            modelBuilder.Entity<CostType>().HasData(new CostType { CostName = "Diğer", Id = 5 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Doğum", AllowedDays = 80, Id = 1 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Babalık", AllowedDays = 5, Id = 2 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Evlilik", AllowedDays = 3, Id = 3 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Evlat Edinme", AllowedDays = 3, Id = 4 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Yıllık", AllowedDays = 14, Id = 5 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Cenaze", AllowedDays = 3, Id = 6 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "İş Arama", AllowedDays = 2, Id = 7 });
            modelBuilder.Entity<PermissionType>().HasData(new PermissionType { PermissionName = "Diğer", AllowedDays = 7, Id = 8 });

            modelBuilder.Entity<Company>().HasData(new Company { Id = 1, Name = "Bilge Adam", Address = "İstanbul/Kadıköy", Sector = "Technology", MailExtension = "bilgeadamboost.com" });

            PasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();        
         
            base.OnModelCreating(modelBuilder);
            //RoleManager = new RoleManager<IdentityRole>(
            //       new RoleStore<IdentityRole>(new HRMVCProjectDbContext()));
            //var roleresult = RoleManager.Create(new IdentityRole("User"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5LLUNOS\\SQLEXPRESS;Initial Catalog=IKBurada;Integrated Security=true;");
            }
        }
        
    }
}
