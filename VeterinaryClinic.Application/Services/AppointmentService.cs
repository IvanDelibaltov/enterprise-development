using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

public sealed class AppointmentService(IRepository<Appointment> appointments, IRepository<Pet> pets, IRepository<Veterinarian> veterinarians)
{
    public IReadOnlyList<AppointmentDto> Appointments() => appointments.GetAll().Select(DtoMapper.Map).ToArray();

    public IReadOnlyList<AppointmentDto> AppointmentsThisMonth(int room, DateTime today) => appointments.GetAll()
        .Where(x => x.RoomNumber == room && x.AppointmentDate.Year == today.Year && x.AppointmentDate.Month == today.Month)
        .OrderBy(x => x.AppointmentDate).Select(DtoMapper.Map).ToArray();

    public AppointmentDto Save(int? id, SaveAppointmentRequest x)
    {
        var pet = pets.Get(x.PetId) ?? throw new ArgumentException("Pet not found");
        var vet = veterinarians.Get(x.VeterinarianId) ?? throw new ArgumentException("Veterinarian not found");
        if (pet.Breed.AnimalType != vet.Specialization.AnimalType)
            throw new ArgumentException("Veterinarian specialization does not match pet species");
        if (x.RoomNumber <= 0 || x.AppointmentDate == default) throw new ArgumentException("Invalid appointment");
        var e = id.HasValue ? appointments.Get(id.Value) ?? throw new KeyNotFoundException() : new Appointment { Id = appointments.NextId() };
        e.Pet = pet; e.Veterinarian = vet; e.RoomNumber = x.RoomNumber; e.AppointmentDate = x.AppointmentDate; e.IsRepeated = x.IsRepeated;
        if (!id.HasValue) appointments.Add(e);
        return DtoMapper.Map(e);
    }
    public void DeleteAppointment(int id) => appointments.Remove(id);
}
