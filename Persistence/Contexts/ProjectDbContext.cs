using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class ProjectDbContext : IdentityDbContext<User,Role,string>
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(u => u.Status == true);
            modelBuilder.Entity<Department>().HasQueryFilter(u => u.Status == true);
            modelBuilder.Entity<Role>().HasQueryFilter(u => u.Status == true);
            modelBuilder.Entity<UserDetail>().HasQueryFilter(u => u.Status == true);

            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Department>()
                .Property(d => d.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Role>()
                .Property(r => r.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<UserDetail>()
                .Property(ud => ud.Status)
                .HasDefaultValue(true);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var datas = ChangeTracker
              .Entries<BaseEntity>();

                foreach (var data in datas)
                {
                    switch (data.State)
                    {
                        case EntityState.Added:
                            data.Entity.Id = Guid.NewGuid();
                            data.Entity.CreatedDate = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            data.Entity.UpdatedDate = DateTime.UtcNow;
                            break;
                    };
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
