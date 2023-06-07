using EfCore.Rest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore.Rest.Database;

public class PeopleDbContext : DbContext
{
    public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigurePersonEntity(modelBuilder.Entity<PersonEntity>());
        ConfigureAddressEntity(modelBuilder.Entity<AddressEntity>());
    }

    private void ConfigureAddressEntity(EntityTypeBuilder<AddressEntity> entity)
    {
        entity.ToTable("Address");
        entity.Property(p => p.AddressLine1)
            .HasMaxLength(100).IsRequired(true);
        entity.Property(p => p.AddressLine2)
            .HasMaxLength(100).IsRequired(true);
        entity.Property(p => p.City)
            .HasMaxLength(50).IsRequired(true);
        entity.Property(p => p.PostCode)
            .HasMaxLength(10).IsRequired(true);
    }

    private static void ConfigurePersonEntity(EntityTypeBuilder<PersonEntity> entity)
    {
        entity.ToTable("Person");
        entity.HasKey(pk => pk.PersonId);
        entity.Property(p => p.FirstName)
            .HasMaxLength(250).IsRequired(true);
        entity.Property(p => p.LastName)
            .HasMaxLength(250).IsRequired(true);
        entity.Property(p => p.PhoneNumber)
            .HasMaxLength(9).IsRequired(false);
        entity.Property(p => p.AddressId)
            .IsRequired(false).HasDefaultValue(null);

        entity.HasOne(o => o.Address)
            .WithMany(m => m.People)
            .HasForeignKey(fk => fk.AddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<PersonEntity> People { get; protected set; }
    public DbSet<AddressEntity> Addresses { get; protected set; }
}