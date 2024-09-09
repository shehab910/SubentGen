using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Subnet> Subnets { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName ="ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName ="USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Subnet>()
                .HasMany(e => e.IpAddresses)
                .WithOne(e => e.Subnet)
                .HasForeignKey(e => e.SubnetId)
                .IsRequired();

            builder.Entity<AppUser>()
                .HasMany(e => e.Subnets)
                .WithMany(e => e.Owners);

            builder.Entity<AppUser>()
                .HasIndex(e => e.UserName)
                .IsUnique();
        }
    }
}
