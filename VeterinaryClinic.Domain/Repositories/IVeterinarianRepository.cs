using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Операции с данными ветеринаров</summary>
public interface IVeterinarianRepository
{
    /// <summary>Создаёт ветеринара</summary>
    Veterinarian CreateVeterinarian(VeterinarianCommand request);
    /// <summary>Обновляет ветеринара</summary>
    Veterinarian UpdateVeterinarian(int id, VeterinarianCommand request);
    /// <summary>Удаляет ветеринара</summary>
    void DeleteVeterinarian(int id);
    /// <summary>Возвращает ветеринаров по виду животного</summary>
    IReadOnlyList<Veterinarian> VeterinariansBySpecies(AnimalType species);
    /// <summary>Возвращает питомцев ветеринара</summary>
    IReadOnlyList<Pet> PetsByVeterinarian(int id);
    /// <summary>Возвращает приёмы ветеринара</summary>
    IReadOnlyList<Appointment> AppointmentsByVeterinarian(int id);
}
