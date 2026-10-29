namespace VeterinaryClinic.Application.Contracts;

/// <summary>Операции сервиса специализаций</summary>
public interface ISpecializationService
{
    /// <summary>Возвращает все специализации</summary>
    IReadOnlyList<SpecializationDto> Specializations();
    /// <summary>Возвращает специализацию по идентификатору</summary>
    SpecializationDto? Get(int id);
    /// <summary>Создаёт специализацию</summary>
    SpecializationDto Create(SaveSpecializationRequest request);
    /// <summary>Обновляет специализацию по идентификатору</summary>
    SpecializationDto Update(int id, SaveSpecializationRequest request);
    /// <summary>Удаляет специализацию по идентификатору</summary>
    void DeleteSpecialization(int id);
    /// <summary>Возвращает ветеринаров по специализации</summary>
    IReadOnlyList<VeterinarianDto> VeterinariansBySpecialization(int id);
}
