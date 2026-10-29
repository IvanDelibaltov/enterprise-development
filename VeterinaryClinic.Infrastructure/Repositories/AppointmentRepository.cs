using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure.Repositories;

/// <summary>Сохраняет приёмы и проверяет связи</summary>
public sealed class AppointmentRepository(
    IRepository<Appointment> appointments,
    IRepository<Pet> pets,
    IRepository<Veterinarian> veterinarians) : IAppointmentRepository
{
    /// <inheritdoc/>
    public Appointment CreateAppointment(AppointmentCommand x)
    {
        var pet = pets.Get(x.PetId)
            ?? throw new ArgumentException("Pet not found");
        var vet = veterinarians.Get(x.VeterinarianId)
            ?? throw new ArgumentException("Veterinarian not found");
        EnsureCompatible(pet, vet);
        return appointments.Create(newId =>
        {
            var entity = new Appointment { Id = newId, Pet = pet, Veterinarian = vet };
            entity.RoomNumber = x.RoomNumber;
            entity.AppointmentDate = x.AppointmentDate;
            entity.IsRepeated = x.IsRepeated;
            return entity;
        });
    }

    /// <inheritdoc/>
    public Appointment UpdateAppointment(int id, AppointmentCommand x)
    {
        var pet = pets.Get(x.PetId)
            ?? throw new ArgumentException("Pet not found");
        var vet = veterinarians.Get(x.VeterinarianId)
            ?? throw new ArgumentException("Veterinarian not found");
        EnsureCompatible(pet, vet);
        return appointments.Update(id, entity =>
        {
            entity.Pet = pet;
            entity.Veterinarian = vet;
            entity.RoomNumber = x.RoomNumber;
            entity.AppointmentDate = x.AppointmentDate;
            entity.IsRepeated = x.IsRepeated;
        });
    }

    /// <inheritdoc/>
    public void DeleteAppointment(int id) => appointments.Remove(id);

    private static void EnsureCompatible(Pet pet, Veterinarian vet)
    {
        if (pet.Breed.AnimalType != vet.Specialization.AnimalType)
            throw new ArgumentException("Veterinarian specialization does not match pet species");
    }


    /// <inheritdoc/>
    public IReadOnlyList<Appointment> AppointmentsThisMonth(int room, DateTime today) =>
        appointments.Find(a => a.RoomNumber == room && a.AppointmentDate.Year == today.Year &&
            a.AppointmentDate.Month == today.Month).OrderBy(a => a.AppointmentDate).ToArray();
}
