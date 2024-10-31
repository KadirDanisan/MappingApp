using Microsoft.EntityFrameworkCore;

namespace MappingApp.Models
{
    public partial class MappingAppDbContext : DbContext
    {
        public MappingAppDbContext(DbContextOptions<MappingAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PointDto> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointDto>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("points_pk");

                entity.ToTable("points");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.PointX).HasColumnName("pointx");
                entity.Property(e => e.PointY).HasColumnName("pointy");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
