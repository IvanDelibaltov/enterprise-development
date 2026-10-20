using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

public sealed class SpecializationService(IRepository<Specialization> specializations, IRepository<Veterinarian> veterinarians)
{
    public IReadOnlyList<SpecializationDto> Specializations() => specializations.GetAll().Select(DtoMapper.Map).ToArray();
    public SpecializationDto Save(int? id, SaveSpecializationRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType)) throw new ArgumentException("Invalid specialization");
        var e = id.HasValue ? specializations.Get(id.Value) ?? throw new KeyNotFoundException() : new Specialization { Id = specializations.NextId() };
        if (id.HasValue && e.AnimalType != x.AnimalType && veterinarians.GetAll().Any(v => v.Specialization.Id == e.Id))
            throw new InvalidOperationException("Cannot change species of a specialization used by veterinarians");
        e.Name = x.Name.Trim(); e.AnimalType = x.AnimalType;
        if (!id.HasValue) specializations.Add(e);
        return DtoMapper.Map(e);
    }
    public void DeleteSpecialization(int id) { if (veterinarians.GetAll().Any(x => x.Specialization.Id == id)) throw new InvalidOperationException("Specialization is in use"); specializations.Remove(id); }
}
