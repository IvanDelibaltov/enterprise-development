using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Операции с данными пород</summary>
public interface IBreedRepository
{
    /// <summary>Создаёт породу</summary>
    Breed CreateBreed(BreedCommand request);
    /// <summary>Обновляет породу</summary>
    Breed UpdateBreed(int id, BreedCommand request);
    /// <summary>Удаляет породу</summary>
    void DeleteBreed(int id);
    /// <summary>Возвращает питомцев выбранной породы</summary>
    IReadOnlyList<Pet> PetsByBreed(int id);
    /// <summary>Считает повторные приёмы для породы</summary>
    int RepeatedAppointments(int breedId);
}
