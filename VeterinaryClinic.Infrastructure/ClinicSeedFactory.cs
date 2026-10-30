using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Data;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Infrastructure;

/// <summary>
/// Заполняет зарегистрированные репозитории тестовыми данными
/// </summary>
public static class ClinicSeedFactory
{
    /// <summary>
    /// Добавляет согласованные тестовые данные в существующие репозитории
    /// </summary>
    /// <param name="provider">Контейнер зарегистрированных зависимостей</param>
    public static void SeedClinicTestData(IServiceProvider provider)
    {
        var breeds = provider.GetRequiredService<IRepository<Breed>>();
        foreach (var x in VeterinaryClinicTestData.Breeds)
        {
            breeds.Add(new Breed { Id = x.Id, Name = x.Name, AnimalType = x.AnimalType });
        }

        var specializations = provider.GetRequiredService<IRepository<Specialization>>();
        foreach (var x in VeterinaryClinicTestData.Specializations)
        {
            specializations.Add(new Specialization { Id = x.Id, Name = x.Name, AnimalType = x.AnimalType });
        }

        var owners = provider.GetRequiredService<IRepository<Owner>>();
        foreach (var x in VeterinaryClinicTestData.Owners)
        {
            owners.Add(new Owner
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                Address = x.Address
            });
        }

        var veterinarians = provider.GetRequiredService<IRepository<Veterinarian>>();
        foreach (var x in VeterinaryClinicTestData.Veterinarians)
        {
            veterinarians.Add(new Veterinarian
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
            });
        }

        var pets = provider.GetRequiredService<IRepository<Pet>>();
        foreach (var x in VeterinaryClinicTestData.Pets)
        {
            var pet = new Pet
            {
                Id = x.Id,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Weight = x.Weight,
                Breed = Require(breeds, x.Breed.Id, "Breed"),
                Owner = x.Owner is null ? null : owners.Get(x.Owner.Id)
            };
            pet.Owner?.Pets.Add(pet);
            pets.Add(pet);
        }

        var appointments = provider.GetRequiredService<IRepository<Appointment>>();
        foreach (var x in VeterinaryClinicTestData.Appointments)
        {
            appointments.Add(new Appointment
            {
                Id = x.Id,
                AppointmentDate = x.AppointmentDate,
                RoomNumber = x.RoomNumber,
                IsRepeated = x.IsRepeated,
                Pet = Require(pets, x.Pet.Id, "Pet"),
                Veterinarian = Require(veterinarians, x.Veterinarian.Id, "Veterinarian")
            });
        }
    }

    private static T Require<T>(IRepository<T> repository, int id, string entityName) where T : class
    {
        return repository.Get(id)
            ?? throw new InvalidOperationException($"Seed data references missing {entityName} with ID {id}");
    }
}
