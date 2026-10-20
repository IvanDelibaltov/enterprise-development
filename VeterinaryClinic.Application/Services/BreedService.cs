using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

public sealed class BreedService(IRepository<Breed> breeds, IRepository<Pet> pets, IRepository<Appointment> appointments)
{
    public IReadOnlyList<BreedDto> Breeds() => breeds.GetAll().Select(DtoMapper.Map).ToArray();
    public int RepeatedAppointments(int breedId) => appointments.GetAll()
        .Count(x => x.IsRepeated && x.Pet.Breed.Id == breedId);
    public BreedDto Save(int? id, SaveBreedRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType)) throw new ArgumentException("Invalid breed");
        var e = id.HasValue ? breeds.Get(id.Value) ?? throw new KeyNotFoundException() : new Breed { Id = breeds.NextId() };
        if (id.HasValue && e.AnimalType != x.AnimalType && pets.GetAll().Any(p => p.Breed.Id == e.Id))
            throw new InvalidOperationException("Cannot change species of a breed used by pets");
        e.Name = x.Name.Trim(); e.AnimalType = x.AnimalType;
        if (!id.HasValue) breeds.Add(e);
        return DtoMapper.Map(e);
    }
    public void DeleteBreed(int id) { if (pets.GetAll().Any(x => x.Breed.Id == id)) throw new InvalidOperationException("Breed is in use"); breeds.Remove(id); }
}
