using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Data;
using VeterinaryClinic.Domain.Models;
namespace VeterinaryClinic.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddClinicRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Breed>>(new InMemoryRepository<Breed>(VeterinaryClinicTestData.Breeds.Select(x => new Breed { Id = x.Id, Name = x.Name, AnimalType = x.AnimalType }), x => x.Id));
        services.AddSingleton<IRepository<Specialization>>(new InMemoryRepository<Specialization>(VeterinaryClinicTestData.Specializations.Select(x => new Specialization { Id = x.Id, Name = x.Name, AnimalType = x.AnimalType }), x => x.Id));
        services.AddSingleton<IRepository<Owner>>(sp => new InMemoryRepository<Owner>(VeterinaryClinicTestData.Owners.Select(x => new Owner { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Patronymic = x.Patronymic, Phone = x.Phone, Address = x.Address }), x => x.Id));
        services.AddSingleton<IRepository<Veterinarian>>(sp => new InMemoryRepository<Veterinarian>(VeterinaryClinicTestData.Veterinarians.Select(x => new Veterinarian { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Patronymic = x.Patronymic, Phone = x.Phone, PassportNumber = x.PassportNumber, BirthDate = x.BirthDate, ExperienceYears = x.ExperienceYears, Specialization = sp.GetRequiredService<IRepository<Specialization>>().Get(x.Specialization.Id)! }), x => x.Id));
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
                Breed = breeds.Get(x.Breed.Id)!,
                Owner = x.Owner is null ? null : owners.Get(x.Owner.Id)
            }).ToArray();
            foreach (var pet in initialPets)
            {
                pet.Owner?.Pets.Add(pet);
            }
            return new InMemoryRepository<Pet>(initialPets, x => x.Id);
        });
        services.AddSingleton<IRepository<Appointment>>(sp => new InMemoryRepository<Appointment>(VeterinaryClinicTestData.Appointments.Select(x => new Appointment { Id = x.Id, AppointmentDate = x.AppointmentDate, RoomNumber = x.RoomNumber, IsRepeated = x.IsRepeated, Pet = sp.GetRequiredService<IRepository<Pet>>().Get(x.Pet.Id)!, Veterinarian = sp.GetRequiredService<IRepository<Veterinarian>>().Get(x.Veterinarian.Id)! }), x => x.Id));
        return services;
    }
}
