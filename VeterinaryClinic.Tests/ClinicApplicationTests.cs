using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Application.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Infrastructure;

namespace VeterinaryClinic.Tests;

public class ClinicApplicationTests
{
    private static ServiceProvider CreateProvider()
    {
        var services = new ServiceCollection();
        services.AddClinicRepositories();
        services.AddScoped<PetService>();
        services.AddScoped<OwnerService>();
        services.AddScoped<BreedService>();
        services.AddScoped<AppointmentService>();
        services.AddScoped<VeterinarianService>();
        return services.BuildServiceProvider();
    }

    [Fact]
    public void CreatedPetAppearsInOwnersAnalyticQuery()
    {
        using var provider = CreateProvider();
        var pets = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<OwnerService>();
        var before = owners.OwnersWithMultiplePets().Select(x => x.Id).ToHashSet();
        var created = pets.Save(null, new SavePetRequest("Test", 1, new DateOnly(2020, 1, 1), 2.5, 8));
        Assert.DoesNotContain(8, before);
        Assert.Contains(8, owners.OwnersWithMultiplePets().Select(x => x.Id));
        pets.DeletePet(created.Id);
        Assert.DoesNotContain(8, owners.OwnersWithMultiplePets().Select(x => x.Id));
    }

    [Fact]
    public void InitialOwnersContainTheirPets()
    {
        using var provider = CreateProvider();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        foreach (var pet in pets.GetAll())
        {
            if (pet.Owner is not null)
            {
                Assert.Contains(pet, pet.Owner.Pets);
                Assert.Same(owners.Get(pet.Owner.Id), pet.Owner);
            }
        }
    }

    [Fact]
    public void CreatingAndReassigningPetUpdatesBothOwners()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        var first = owners.Get(1)!;
        var second = owners.Get(2)!;
        var created = service.Save(null, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 1));
        var pet = pets.Get(created.Id)!;
        Assert.Contains(pet, first.Pets);

        service.Save(created.Id, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 2));
        Assert.DoesNotContain(pet, first.Pets);
        Assert.Contains(pet, second.Pets);
        Assert.Same(second, pet.Owner);

        service.Save(created.Id, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, null));
        Assert.DoesNotContain(pet, second.Pets);
        Assert.Null(pet.Owner);
    }

    [Fact]
    public void DeletingPetRemovesItFromOwnerCollection()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var owner = owners.Get(1)!;
        var created = service.Save(null, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 1));
        Assert.Contains(owner.Pets, pet => pet.Id == created.Id);
        service.DeletePet(created.Id);
        Assert.DoesNotContain(owner.Pets, pet => pet.Id == created.Id);
    }

    [Fact]
    public void DeletingOwnerUnlinksTheirPets()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<OwnerService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        var owner = owners.Get(1)!;
        var relatedPets = pets.GetAll().Where(pet => pet.Owner?.Id == 1).ToArray();
        Assert.NotEmpty(relatedPets);
        service.DeleteOwner(1);
        Assert.Empty(owner.Pets);
        Assert.All(relatedPets, pet => Assert.Null(pet.Owner));
        Assert.Null(owners.Get(1));
    }

    [Fact]
    public void InvalidOwnerIsRejected()
    {
        using var provider = CreateProvider();
        var pets = provider.GetRequiredService<PetService>();
        Assert.Throws<ArgumentException>(() => pets.Save(null,
            new SavePetRequest("Test", 1, new DateOnly(2020, 1, 1), 2.5, 99999)));
    }

    [Fact]
    public void ChangingSpeciesOfUsedBreedIsRejected()
    {
        using var provider = CreateProvider();
        var breeds = provider.GetRequiredService<BreedService>();
        var existing = breeds.Breeds().First(x => x.Id == 1);
        var different = Enum.GetValues<VeterinaryClinic.Domain.Shared.Enums.AnimalType>()
            .First(x => x != existing.AnimalType);
        Assert.Throws<InvalidOperationException>(() => breeds.Save(1,
            new SaveBreedRequest(existing.Name, different)));
    }
}
