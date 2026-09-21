namespace VeterinaryClinic.Tests;

using VeterinaryClinic.Domain.Data;
using VeterinaryClinic.Domain.Shared.Enums;

/// <summary>
/// Проверки аналитических запросов ветеринарной клиники
/// </summary>
public class VeterinaryClinicTests
{
    /// <summary>
    /// Ветеринары, специализирующиеся на выбранном виде животных
    /// </summary>
    [Fact]
    public void GetVeterinariansByAnimalType()
    {
        var animalType = AnimalType.Dog;

        var result = VeterinaryClinicTestData.Veterinarians
            .Where(veterinarian => veterinarian.Specialization.AnimalType == animalType)
            .OrderBy(veterinarian => veterinarian.LastName)
            .ThenBy(veterinarian => veterinarian.FirstName)
            .ToArray();

        var expected = new[]
        {
            "Андреев",
            "Ефремова",
            "Зимина"
        };

        Assert.Equal(expected, result.Select(veterinarian => veterinarian.LastName).ToArray());
    }

    /// <summary>
    /// Питомцы, записанные на приём к указанному врачу
    /// </summary>
    [Fact]
    public void GetPetsByVeterinarian()
    {
        var veterinarianId = 1;

        var result = VeterinaryClinicTestData.Appointments
            .Where(appointment => appointment.Veterinarian.Id == veterinarianId)
            .Select(appointment => appointment.Pet)
            .DistinctBy(pet => pet.Id)
            .OrderBy(pet => pet.Name)
            .ToArray();

       var expected = new[]
{
    "Альма",
    "Арчи",
    "Бим"
};
        Assert.Equal(expected, result.Select(pet => pet.Name).ToArray());
    }

    /// <summary>
    /// Количество повторных приёмов выбранной породы
    /// </summary>
    [Fact]
    public void GetRepeatedAppointmentsByBreed()
    {
        var breedId = 1;

        var result = VeterinaryClinicTestData.Appointments
            .Count(appointment =>
                appointment.IsRepeated &&
                appointment.Pet.Breed.Id == breedId);

        Assert.Equal(6, result);
    }

    /// <summary>
    /// Владельцы 1+ питомца
    /// </summary>
    [Fact]
    public void GetOwnersWithMoreThanOnePet()
    {
        var result = VeterinaryClinicTestData.Owners
            .Where(owner => owner.Pets.Count > 1)
            .OrderBy(owner => owner.LastName)
            .ThenBy(owner => owner.FirstName)
            .ThenBy(owner => owner.Patronymic)
            .ToArray();

        var expected = new[]
        {
            "Алексеев",
            "Беляева",
            "Васильев",
            "Громова",
            "Дорофеев",
            "Ершова",
            "Жуков",
            "Иванов",
            "Крылова"
        };

        Assert.Equal(expected, result.Select(owner => owner.LastName).ToArray());
    }

    /// <summary>
    /// Приёмы за текущий месяц в выбранном кабинете
    /// </summary>
    [Fact]
    public void GetAppointmentsForCurrentMonthByRoom()
    {
        var roomNumber = 101;
        var today = DateTime.Today;
        var monthStart = new DateTime(today.Year, today.Month, 1);
        var nextMonthStart = monthStart.AddMonths(1);

        var result = VeterinaryClinicTestData.Appointments
            .Where(appointment =>
                appointment.RoomNumber == roomNumber &&
                appointment.AppointmentDate >= monthStart &&
                appointment.AppointmentDate < nextMonthStart)
            .OrderBy(appointment => appointment.AppointmentDate)
            .ToArray();

        Assert.Equal(3, result.Length);
        Assert.All(result, appointment => Assert.Equal(roomNumber, appointment.RoomNumber));
        Assert.All(result, appointment => Assert.True(
            appointment.AppointmentDate >= monthStart &&
            appointment.AppointmentDate < nextMonthStart));
    }
}
