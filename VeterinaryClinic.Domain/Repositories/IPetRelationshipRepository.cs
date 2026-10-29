using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Управляет связями питомцев и владельцев</summary>
public interface IPetRelationshipRepository
{
    /// <summary>Назначает владельца питомцу и обновляет обе стороны связи</summary>
    void AssignOwner(Pet pet, Owner? owner);

    /// <summary>Удаляет связь владельца со всеми его питомцами</summary>
    void UnlinkOwner(Owner owner);
}
