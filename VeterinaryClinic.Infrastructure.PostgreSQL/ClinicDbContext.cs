using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

public sealed class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : DbContext(options)
{
    public DbSet<Breed> Breeds => Set<Breed>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Veterinarian> Veterinarians => Set<Veterinarian>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breed>(b =>
        {
            b.ToTable("breeds");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.AnimalType).HasConversion<string>();
        });
        modelBuilder.Entity<Specialization>(b =>
        {
            b.ToTable("specializations");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.AnimalType).HasConversion<string>();
        });
        modelBuilder.Entity<Owner>(b =>
        {
            b.ToTable("owners");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Ignore(x => x.Pets);
        });
        modelBuilder.Entity<Veterinarian>(b =>
        {
            b.ToTable("veterinarians");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.HasOne(x => x.Specialization).WithMany().HasForeignKey("SpecializationId").OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Pet>(b =>
        {
            b.ToTable("pets");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.HasOne(x => x.Breed).WithMany().HasForeignKey("BreedId").OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Owner).WithMany().HasForeignKey("OwnerId").OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Appointment>(b =>
        {
            b.ToTable("appointments");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.HasOne(x => x.Pet).WithMany().HasForeignKey("PetId").OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Veterinarian).WithMany().HasForeignKey("VeterinarianId").OnDelete(DeleteBehavior.Restrict);
            b.Property(x => x.AppointmentDate).HasColumnType("timestamp without time zone");
            b.HasIndex(x => new { x.AppointmentDate, x.RoomNumber }).IsUnique();
        });
    }
}
