using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Операции с данными питомцев</summary>
public interface IPetRepository
{
    /// <summary>Создаёт питомца</summary>
    Pet CreatePet(PetCommand request);
    /// <summary>Обновляет питомца</summary>
    Pet UpdatePet(int id, PetCommand request);
    /// <summary>Удаляет питомца</summary>
    void DeletePet(int id);
    /// <summary>Возвращает владельца питомца</summary>
    Owner? GetPetOwner(int petId);
    /// <summary>Возвращает приёмы питомца</summary>
    IReadOnlyList<Appointment> AppointmentsByPet(int petId);
}
