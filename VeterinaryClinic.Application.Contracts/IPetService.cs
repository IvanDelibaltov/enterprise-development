namespace VeterinaryClinic.Application.Contracts;

/// <summary>Операции сервиса питомцев</summary>
public interface IPetService
{
    /// <summary>Возвращает всех питомцев</summary>
    IReadOnlyList<PetDto> Pets();
    /// <summary>Возвращает питомца по идентификатору</summary>
    PetDto? Get(int id);
    /// <summary>Создаёт питомца</summary>
    PetDto Create(SavePetRequest request);
    /// <summary>Обновляет питомца по идентификатору</summary>
    PetDto Update(int id, SavePetRequest request);
    /// <summary>Удаляет питомца по идентификатору</summary>
    void DeletePet(int id);
    /// <summary>Возвращает владельца питомца</summary>
    OwnerDto? OwnerByPet(int id);
    /// <summary>Возвращает приёмы питомца</summary>
    IReadOnlyList<AppointmentDto> AppointmentsByPet(int id);
}
