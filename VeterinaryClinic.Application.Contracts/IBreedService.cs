namespace VeterinaryClinic.Application.Contracts;

/// <summary>Операции сервиса пород</summary>
public interface IBreedService
{
    /// <summary>Возвращает все породы</summary>
    IReadOnlyList<BreedDto> Breeds();
    /// <summary>Возвращает породу по идентификатору</summary>
    BreedDto? Get(int id);
    /// <summary>Создаёт породу</summary>
    BreedDto Create(SaveBreedRequest request);
    /// <summary>Обновляет породу по идентификатору</summary>
    BreedDto Update(int id, SaveBreedRequest request);
    /// <summary>Удаляет породу по идентификатору</summary>
    void DeleteBreed(int id);
    /// <summary>Считает повторные приёмы по породе</summary>
    int RepeatedAppointments(int breedId);
    /// <summary>Возвращает питомцев по породе</summary>
    IReadOnlyList<PetDto> PetsByBreed(int id);
}
