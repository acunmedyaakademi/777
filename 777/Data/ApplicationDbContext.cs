using _777.Data.Entities;
using _777.Data.Entities.Common;
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
        public DbSet<TextApp> TextApps { get; set; }
        public DbSet<InspireMessage> InspireMessages { get; set; }
        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<IBaseClass>(); 

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.UpdatedOn = DateTime.Now;
                        break;
                    case EntityState.Added:
                        data.Entity.CreatedOn = DateTime.Now;
                        data.Entity.UpdatedOn = DateTime.Now;
                        break;

                    default:
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
