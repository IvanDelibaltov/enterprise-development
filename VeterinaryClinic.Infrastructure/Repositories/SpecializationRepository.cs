using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Infrastructure.Repositories;

/// <summary>Сохраняет специализации и проверяет связи</summary>
public sealed class SpecializationRepository(
    IRepository<Specialization> specializations,
    IRepository<Veterinarian> veterinarians) : ISpecializationRepository
{
    /// <inheritdoc/>
    public Specialization CreateSpecialization(SpecializationCommand x)
    {
        EnsureUniqueSpecialization(x.Name, x.AnimalType);
        return specializations.Create(newId =>
        {
            var entity = new Specialization { Id = newId };
            entity.Name = x.Name.Trim();
            entity.AnimalType = x.AnimalType;
            return entity;
        });
    }

    /// <inheritdoc/>
    public Specialization UpdateSpecialization(int id, SpecializationCommand x)
    {
        var current = specializations.Get(id) ?? throw new KeyNotFoundException($"Specialization {id} not found");
        EnsureUniqueSpecialization(x.Name, x.AnimalType, id);
        if (current.AnimalType != x.AnimalType && veterinarians.Any(v => v.Specialization.Id == id))
            throw new InvalidOperationException("Cannot change species of a specialization in use");
        return specializations.Update(id, entity =>
        {
            entity.Name = x.Name.Trim();
            entity.AnimalType = x.AnimalType;
        });
    }

    /// <inheritdoc/>
    public void DeleteSpecialization(int id)
    {
        if (veterinarians.Any(v => v.Specialization.Id == id))
            throw new InvalidOperationException("Specialization is in use");
        specializations.Remove(id);
    }

    private void EnsureUniqueSpecialization(
        string name,
        AnimalType type,
        int? exceptId = null)
    {
        if (specializations.Any(s => s.Id != exceptId && s.AnimalType == type &&
            string.Equals(s.Name, name.Trim(), StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Specialization already exists");
    }

    /// <inheritdoc/>
    public IReadOnlyList<Veterinarian> VeterinariansBySpecialization(int id)
    {
        if (specializations.Get(id) is null) throw new KeyNotFoundException($"Specialization {id} not found");
        return veterinarians.Find(v => v.Specialization.Id == id);
    }
}
