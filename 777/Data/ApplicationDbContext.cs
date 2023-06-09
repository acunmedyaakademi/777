using _777.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace _777.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApp,RoleApp,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserApp> Users { get; set; }
        public DbSet<RoleApp> Roles { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<InspireMessage> InspireMessages { get; set; }
    }
}