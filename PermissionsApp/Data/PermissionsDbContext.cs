using Microsoft.EntityFrameworkCore;
using PermissionsApp.Domain;

namespace PermissionsApp.Data
{
    public class PermissionsDbContext : DbContext
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la relación entre Permission y PermissionType
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.PermissionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración adicional si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}