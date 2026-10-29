using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Операции с данными специализаций</summary>
public interface ISpecializationRepository
{
    /// <summary>Создаёт специализацию</summary>
    Specialization CreateSpecialization(SpecializationCommand request);
    /// <summary>Обновляет специализацию</summary>
    Specialization UpdateSpecialization(int id, SpecializationCommand request);
    /// <summary>Удаляет специализацию</summary>
    void DeleteSpecialization(int id);
    /// <summary>Возвращает ветеринаров выбранной специализации</summary>
    IReadOnlyList<Veterinarian> VeterinariansBySpecialization(int id);
}
