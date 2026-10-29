using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Infrastructure.Repositories;
using VeterinaryClinic.Domain.Data;
using VeterinaryClinic.Domain.Models;
namespace VeterinaryClinic.Infrastructure;

/// <summary>
/// Регистрирует репозитории и начальные данные клиники
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Добавляет репозитории клиники в контейнер зависимостей
    /// </summary>
    public static IServiceCollection AddClinicRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Breed>>(
            new InMemoryRepository<Breed>(
                VeterinaryClinicTestData.Breeds.Select(x => new Breed
                {
                    Id = x.Id,
                    Name = x.Name,
                    AnimalType = x.AnimalType
                }),
                x => x.Id));

        services.AddSingleton<IRepository<Specialization>>(
            new InMemoryRepository<Specialization>(
                VeterinaryClinicTestData.Specializations.Select(x => new Specialization
                {
                    Id = x.Id,
                    Name = x.Name,
                    AnimalType = x.AnimalType
                }),
                x => x.Id));

        services.AddSingleton<IRepository<Owner>>(
            new InMemoryRepository<Owner>(
                VeterinaryClinicTestData.Owners.Select(x => new Owner
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Patronymic = x.Patronymic,
                    Phone = x.Phone,
                    Address = x.Address
                }),
                x => x.Id));

        services.AddSingleton<IRepository<Veterinarian>>(sp =>
        {
            var specializations = sp.GetRequiredService<IRepository<Specialization>>();
            return new InMemoryRepository<Veterinarian>(
                VeterinaryClinicTestData.Veterinarians.Select(x => new Veterinarian
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Patronymic = x.Patronymic,
                    Phone = x.Phone,
                    PassportNumber = x.PassportNumber,
                    BirthDate = x.BirthDate,
                    ExperienceYears = x.ExperienceYears,
                    Specialization = Require(specializations, x.Specialization.Id, "Specialization")
                }),
                x => x.Id);
        });

        services.AddSingleton<IRepository<Pet>>(sp =>
        {
            var owners = sp.GetRequiredService<IRepository<Owner>>();
            var breeds = sp.GetRequiredService<IRepository<Breed>>();
            var initialPets = VeterinaryClinicTestData.Pets.Select(x => new Pet
            {
                Id = x.Id,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Weight = x.Weight,
                Breed = Require(breeds, x.Breed.Id, "Breed"),
                Owner = x.Owner is null ? null : owners.Get(x.Owner.Id)
            }).ToArray();
            foreach (var pet in initialPets)
            {
                pet.Owner?.Pets.Add(pet);
            }
            return new InMemoryRepository<Pet>(initialPets, x => x.Id);
        });
        services.AddSingleton<IRepository<Appointment>>(sp =>
            new InMemoryRepository<Appointment>(
                VeterinaryClinicTestData.Appointments.Select(x => new Appointment
        {
            Id = x.Id, AppointmentDate = x.AppointmentDate, RoomNumber = x.RoomNumber,
            IsRepeated = x.IsRepeated,
            Pet = Require(sp.GetRequiredService<IRepository<Pet>>(), x.Pet.Id, "Pet"),
            Veterinarian = Require(sp.GetRequiredService<IRepository<Veterinarian>>(), x.Veterinarian.Id,
                "Veterinarian")
        }), x => x.Id));
        services.AddSingleton<IPetRelationshipRepository, PetRelationshipRepository>();
        services.AddScoped<IBreedRepository, BreedRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        return services;
    }

    private static T Require<T>(IRepository<T> repository, int id, string entityName) where T : class
    {
        return repository.Get(id)
            ?? throw new InvalidOperationException($"Seed data references missing {entityName} with ID {id}");
    }
}
