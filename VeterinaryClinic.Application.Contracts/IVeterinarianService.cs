namespace VeterinaryClinic.Application.Contracts;

/// <summary>Операции сервиса ветеринаров</summary>
public interface IVeterinarianService
{
    /// <summary>Возвращает всех ветеринаров</summary>
    IReadOnlyList<VeterinarianDto> Veterinarians();
    /// <summary>Возвращает ветеринара по идентификатору</summary>
    VeterinarianDto? Get(int id);
    /// <summary>Создаёт ветеринара</summary>
    VeterinarianDto Create(SaveVeterinarianRequest request);
    /// <summary>Обновляет ветеринара по идентификатору</summary>
    VeterinarianDto Update(int id, SaveVeterinarianRequest request);
    /// <summary>Удаляет ветеринара по идентификатору</summary>
    void DeleteVeterinarian(int id);
    /// <summary>Возвращает ветеринаров по виду животного</summary>
    IReadOnlyList<VeterinarianDto> VeterinariansBySpecies(VeterinaryClinic.Domain.Shared.Enums.AnimalType species);
    /// <summary>Возвращает питомцев ветеринара</summary>
    IReadOnlyList<PetDto> PetsByVeterinarian(int id);
    /// <summary>Возвращает приёмы ветеринара</summary>
    IReadOnlyList<AppointmentDto> AppointmentsByVeterinarian(int id);
}
