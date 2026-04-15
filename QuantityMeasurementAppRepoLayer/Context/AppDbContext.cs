using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppRepoLayer.Context;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<QuantityMeasurementHistoryEntity> MeasurementHistories { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserEntity>().ToTable("Users");

        modelBuilder.Entity<QuantityMeasurementHistoryEntity>().ToTable("MeasurementHistory");

        base.OnModelCreating(modelBuilder);
    }
}