namespace VeterinaryClinic.Domain.Data;

using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

/// <summary>
/// Тестовые данные ветеринарной клиники
/// </summary>
public static class VeterinaryClinicTestData
{
    /// <summary>
    /// Список пород
    /// </summary>
    public static List<Breed> Breeds { get; } =
    [
        new() { Id = 1, Name = "Лабрадор", AnimalType = AnimalType.Dog },
        new() { Id = 2, Name = "Немецкая овчарка", AnimalType = AnimalType.Dog },
        new() { Id = 3, Name = "Сиамская", AnimalType = AnimalType.Cat },
        new() { Id = 4, Name = "Британская короткошёрстная", AnimalType = AnimalType.Cat },
        new() { Id = 5, Name = "Корелла", AnimalType = AnimalType.Bird },
        new() { Id = 6, Name = "Волнистый попугай", AnimalType = AnimalType.Bird },
        new() { Id = 7, Name = "Карликовый кролик", AnimalType = AnimalType.Rabbit },
        new() { Id = 8, Name = "Баран", AnimalType = AnimalType.Rabbit },
        new() { Id = 9, Name = "Королевский питон", AnimalType = AnimalType.Reptile },
        new() { Id = 10, Name = "Красноухая черепаха", AnimalType = AnimalType.Reptile },
        new() { Id = 11, Name = "Сирийский хомяк", AnimalType = AnimalType.Rodent }
    ];

    /// <summary>
    /// Список владельцев
    /// </summary>
    public static List<Owner> Owners { get; } =
    [
        new() { Id = 1, LastName = "Алексеев", FirstName = "Антон", Patronymic = "Игоревич", Address = "ул. Лесная, 12", Phone = "+79990000001" },
        new() { Id = 2, LastName = "Беляева", FirstName = "Мария", Patronymic = "Сергеевна", Address = "ул. Центральная, 8", Phone = "+79990000002" },
        new() { Id = 3, LastName = "Васильев", FirstName = "Дмитрий", Patronymic = "Олегович", Address = "ул. Садовая, 15", Phone = "+79990000003" },
        new() { Id = 4, LastName = "Громова", FirstName = "Елена", Patronymic = "Андреевна", Address = "ул. Полевая, 21", Phone = "+79990000004" },
        new() { Id = 5, LastName = "Дорофеев", FirstName = "Илья", Patronymic = "Павлович", Address = "ул. Молодёжная, 4", Phone = "+79990000005" },
        new() { Id = 6, LastName = "Ершова", FirstName = "Ольга", Patronymic = "Викторовна", Address = "ул. Школьная, 19", Phone = "+79990000006" },
        new() { Id = 7, LastName = "Жуков", FirstName = "Максим", Patronymic = "Романович", Address = "ул. Новая, 33", Phone = "+79990000007" },
        new() { Id = 8, LastName = "Захарова", FirstName = "Ирина", Patronymic = "Денисовна", Address = "ул. Парковая, 2", Phone = "+79990000008" },
        new() { Id = 9, LastName = "Иванов", FirstName = "Пётр", Patronymic = "Алексеевич", Address = "ул. Заречная, 7", Phone = "+79990000009" },
        new() { Id = 10, LastName = "Крылова", FirstName = "Наталья", Patronymic = "Михайловна", Address = "ул. Речная, 10", Phone = "+79990000010" }
    ];

    /// <summary>
    /// Список питомцев
    /// </summary>
    public static List<Pet> Pets { get; } =
    [
        new() { Id = 1, Name = "Бим", AnimalType = AnimalType.Dog, Breed = Breeds[0], BirthDate = new DateTime(2020, 3, 12), Weight = 28.5m, Owner = Owners[0] },
        new() { Id = 2, Name = "Альма", AnimalType = AnimalType.Dog, Breed = Breeds[1], BirthDate = new DateTime(2019, 7, 5), Weight = 31.2m, Owner = Owners[0] },
        new() { Id = 3, Name = "Барсик", AnimalType = AnimalType.Cat, Breed = Breeds[2], BirthDate = new DateTime(2021, 1, 18), Weight = 4.7m, Owner = Owners[1] },
        new() { Id = 4, Name = "Луна", AnimalType = AnimalType.Cat, Breed = Breeds[3], BirthDate = new DateTime(2022, 6, 23), Weight = 5.1m, Owner = Owners[1] },
        new() { Id = 5, Name = "Кеша", AnimalType = AnimalType.Bird, Breed = Breeds[4], BirthDate = new DateTime(2022, 2, 10), Weight = 0.09m, Owner = Owners[2] },
        new() { Id = 6, Name = "Ричи", AnimalType = AnimalType.Bird, Breed = Breeds[5], BirthDate = new DateTime(2023, 4, 2), Weight = 0.04m, Owner = Owners[3] },
        new() { Id = 7, Name = "Снежок", AnimalType = AnimalType.Rabbit, Breed = Breeds[6], BirthDate = new DateTime(2021, 11, 14), Weight = 1.8m, Owner = Owners[4] },
        new() { Id = 8, Name = "Пушок", AnimalType = AnimalType.Rabbit, Breed = Breeds[7], BirthDate = new DateTime(2020, 9, 9), Weight = 2.2m, Owner = Owners[5] },
        new() { Id = 9, Name = "Кобра", AnimalType = AnimalType.Reptile, Breed = Breeds[8], BirthDate = new DateTime(2018, 5, 30), Weight = 1.6m, Owner = Owners[6] },
        new() { Id = 10, Name = "Тортила", AnimalType = AnimalType.Reptile, Breed = Breeds[9], BirthDate = new DateTime(2017, 8, 17), Weight = 0.8m, Owner = Owners[7] },
        new() { Id = 11, Name = "Арчи", AnimalType = AnimalType.Dog, Breed = Breeds[0], BirthDate = new DateTime(2022, 4, 1), Weight = 24.3m, Owner = Owners[8] },
        new() { Id = 12, Name = "Джек", AnimalType = AnimalType.Dog, Breed = Breeds[0], BirthDate = new DateTime(2021, 12, 20), Weight = 26.1m, Owner = Owners[9] },
        new() { Id = 13, Name = "Грей", AnimalType = AnimalType.Dog, Breed = Breeds[1], BirthDate = new DateTime(2020, 10, 11), Weight = 29.4m, Owner = Owners[8] },
        new() { Id = 14, Name = "Марта", AnimalType = AnimalType.Cat, Breed = Breeds[2], BirthDate = new DateTime(2023, 3, 7), Weight = 3.9m, Owner = Owners[9] },
        new() { Id = 15, Name = "Оскар", AnimalType = AnimalType.Cat, Breed = Breeds[3], BirthDate = new DateTime(2019, 12, 28), Weight = 6.0m, Owner = Owners[3] },
        new() { Id = 16, Name = "Тиша", AnimalType = AnimalType.Dog, Breed = Breeds[0], BirthDate = new DateTime(2022, 8, 6), Weight = 22.7m, Owner = Owners[0] },
        new() { Id = 17, Name = "Нора", AnimalType = AnimalType.Rabbit, Breed = Breeds[6], BirthDate = new DateTime(2023, 1, 25), Weight = 1.5m, Owner = Owners[4] },
        new() { Id = 18, Name = "Яша", AnimalType = AnimalType.Bird, Breed = Breeds[5], BirthDate = new DateTime(2021, 5, 19), Weight = 0.05m, Owner = Owners[5] },
        new() { Id = 19, Name = "Феня", AnimalType = AnimalType.Reptile, Breed = Breeds[9], BirthDate = new DateTime(2020, 7, 21), Weight = 0.9m, Owner = Owners[6] },
        new() { Id = 20, Name = "Рокки", AnimalType = AnimalType.Dog, Breed = Breeds[1], BirthDate = new DateTime(2019, 2, 3), Weight = 32.0m, Owner = Owners[2] },
        new() { Id = 21, Name = "Пуфик", AnimalType = AnimalType.Rodent, Breed = Breeds[10], BirthDate = new DateTime(2024, 2, 14), Weight = 0.12m, Owner = Owners[6] }
    ];

    /// <summary>
    /// Список специализаций
    /// </summary>
    public static List<Specialization> Specializations { get; } =
    [
        new() { Id = 1, Name = "Терапевт", AnimalType = AnimalType.Dog },
        new() { Id = 2, Name = "Хирург", AnimalType = AnimalType.Cat },
        new() { Id = 3, Name = "Орнитолог", AnimalType = AnimalType.Bird },
        new() { Id = 4, Name = "Ратолог", AnimalType = AnimalType.Rabbit },
        new() { Id = 5, Name = "Герпетолог", AnimalType = AnimalType.Reptile },
        new() { Id = 6, Name = "Кардиолог", AnimalType = AnimalType.Dog },
        new() { Id = 7, Name = "Дерматолог", AnimalType = AnimalType.Cat },
        new() { Id = 8, Name = "Стоматолог", AnimalType = AnimalType.Dog },
        new() { Id = 9, Name = "Офтальмолог", AnimalType = AnimalType.Bird },
        new() { Id = 10, Name = "Диетолог", AnimalType = AnimalType.Rabbit },
        new() { Id = 11, Name = "Родентолог", AnimalType = AnimalType.Rodent }
    ];

    /// <summary>
    /// Список ветеринаров
    /// </summary>
    public static List<Veterinarian> Veterinarians { get; } =
    [
        new() { Id = 1, PassportNumber = "4010 100001", LastName = "Андреев", FirstName = "Иван", Patronymic = "Сергеевич", BirthYear = 1985, Specialization = Specializations[0], ExperienceYears = 12 },
        new() { Id = 2, PassportNumber = "4010 100002", LastName = "Борисова", FirstName = "Анна", Patronymic = "Игоревна", BirthYear = 1988, Specialization = Specializations[1], ExperienceYears = 10 },
        new() { Id = 3, PassportNumber = "4010 100003", LastName = "Волков", FirstName = "Дмитрий", Patronymic = "Алексеевич", BirthYear = 1982, Specialization = Specializations[2], ExperienceYears = 15 },
        new() { Id = 4, PassportNumber = "4010 100004", LastName = "Гаврилова", FirstName = "Ольга", Patronymic = "Петровна", BirthYear = 1990, Specialization = Specializations[3], ExperienceYears = 8 },
        new() { Id = 5, PassportNumber = "4010 100005", LastName = "Данилов", FirstName = "Максим", Patronymic = "Романович", BirthYear = 1984, Specialization = Specializations[4], ExperienceYears = 13 },
        new() { Id = 6, PassportNumber = "4010 100006", LastName = "Ефремова", FirstName = "Светлана", Patronymic = "Владимировна", BirthYear = 1987, Specialization = Specializations[5], ExperienceYears = 11 },
        new() { Id = 7, PassportNumber = "4010 100007", LastName = "Жданов", FirstName = "Алексей", Patronymic = "Олегович", BirthYear = 1991, Specialization = Specializations[6], ExperienceYears = 7 },
        new() { Id = 8, PassportNumber = "4010 100008", LastName = "Зимина", FirstName = "Марина", Patronymic = "Андреевна", BirthYear = 1986, Specialization = Specializations[7], ExperienceYears = 12 },
        new() { Id = 9, PassportNumber = "4010 100009", LastName = "Ильин", FirstName = "Роман", Patronymic = "Викторович", BirthYear = 1989, Specialization = Specializations[8], ExperienceYears = 9 },
        new() { Id = 10, PassportNumber = "4010 100010", LastName = "Ковалёва", FirstName = "Екатерина", Patronymic = "Денисовна", BirthYear = 1992, Specialization = Specializations[9], ExperienceYears = 6 },
        new() { Id = 11, PassportNumber = "4010 100011", LastName = "Лебедева", FirstName = "Наталья", Patronymic = "Ильинична", BirthYear = 1993, Specialization = Specializations[10], ExperienceYears = 5 }
    ];

    /// <summary>
    /// Список записей на приём
    /// </summary>
    public static List<Appointment> Appointments { get; } =
    [
        new() { Id = 1, Pet = Pets[0], Veterinarian = Veterinarians[0], AppointmentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 2), RoomNumber = 101, IsRepeated = false },
        new() { Id = 2, Pet = Pets[1], Veterinarian = Veterinarians[0], AppointmentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 5), RoomNumber = 101, IsRepeated = true },
        new() { Id = 3, Pet = Pets[10], Veterinarian = Veterinarians[0], AppointmentDate = DateTime.Today.AddDays(-5), RoomNumber = 102, IsRepeated = true },
        new() { Id = 4, Pet = Pets[11], Veterinarian = Veterinarians[5], AppointmentDate = DateTime.Today.AddDays(-4), RoomNumber = 103, IsRepeated = true },
        new() { Id = 5, Pet = Pets[15], Veterinarian = Veterinarians[7], AppointmentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 8), RoomNumber = 101, IsRepeated = true },
        new() { Id = 6, Pet = Pets[0], Veterinarian = Veterinarians[0], AppointmentDate = DateTime.Today.AddDays(-2), RoomNumber = 104, IsRepeated = true },
        new() { Id = 7, Pet = Pets[2], Veterinarian = Veterinarians[1], AppointmentDate = DateTime.Today.AddDays(-1), RoomNumber = 201, IsRepeated = false },
        new() { Id = 8, Pet = Pets[3], Veterinarian = Veterinarians[1], AppointmentDate = DateTime.Today.AddDays(-15), RoomNumber = 201, IsRepeated = true },
        new() { Id = 9, Pet = Pets[4], Veterinarian = Veterinarians[2], AppointmentDate = DateTime.Today.AddDays(-12), RoomNumber = 301, IsRepeated = false },
        new() { Id = 10, Pet = Pets[5], Veterinarian = Veterinarians[2], AppointmentDate = DateTime.Today.AddDays(-20), RoomNumber = 301, IsRepeated = true },
        new() { Id = 11, Pet = Pets[6], Veterinarian = Veterinarians[3], AppointmentDate = DateTime.Today.AddDays(-25), RoomNumber = 401, IsRepeated = false },
        new() { Id = 12, Pet = Pets[7], Veterinarian = Veterinarians[3], AppointmentDate = DateTime.Today.AddDays(-18), RoomNumber = 401, IsRepeated = true },
        new() { Id = 13, Pet = Pets[8], Veterinarian = Veterinarians[4], AppointmentDate = DateTime.Today.AddDays(-30), RoomNumber = 501, IsRepeated = false },
        new() { Id = 14, Pet = Pets[9], Veterinarian = Veterinarians[4], AppointmentDate = DateTime.Today.AddDays(-40), RoomNumber = 501, IsRepeated = true },
        new() { Id = 15, Pet = Pets[12], Veterinarian = Veterinarians[5], AppointmentDate = DateTime.Today.AddMonths(-1), RoomNumber = 103, IsRepeated = true },
        new() { Id = 16, Pet = Pets[13], Veterinarian = Veterinarians[6], AppointmentDate = DateTime.Today.AddMonths(-1).AddDays(-2), RoomNumber = 201, IsRepeated = false },
        new() { Id = 17, Pet = Pets[14], Veterinarian = Veterinarians[6], AppointmentDate = DateTime.Today.AddMonths(-1).AddDays(-4), RoomNumber = 201, IsRepeated = true },
        new() { Id = 18, Pet = Pets[16], Veterinarian = Veterinarians[3], AppointmentDate = DateTime.Today.AddMonths(-1).AddDays(-6), RoomNumber = 401, IsRepeated = false },
        new() { Id = 19, Pet = Pets[17], Veterinarian = Veterinarians[8], AppointmentDate = DateTime.Today.AddMonths(-1).AddDays(-8), RoomNumber = 301, IsRepeated = false },
        new() { Id = 20, Pet = Pets[18], Veterinarian = Veterinarians[4], AppointmentDate = DateTime.Today.AddMonths(-1).AddDays(-10), RoomNumber = 501, IsRepeated = true },
        new() { Id = 21, Pet = Pets[19], Veterinarian = Veterinarians[5], AppointmentDate = DateTime.Today.AddMonths(-1).AddDays(-12), RoomNumber = 103, IsRepeated = false },
        new() { Id = 22, Pet = Pets[0], Veterinarian = Veterinarians[0], AppointmentDate = DateTime.Today.AddMonths(1), RoomNumber = 101, IsRepeated = true },
        new() { Id = 23, Pet = Pets[1], Veterinarian = Veterinarians[0], AppointmentDate = DateTime.Today.AddMonths(1).AddDays(1), RoomNumber = 101, IsRepeated = false },
        new() { Id = 24, Pet = Pets[10], Veterinarian = Veterinarians[0], AppointmentDate = DateTime.Today.AddMonths(1).AddDays(2), RoomNumber = 102, IsRepeated = true }
    ];

    static VeterinaryClinicTestData()
    {
        foreach (var pet in Pets)
        {
            pet.Owner.Pets.Add(pet);
        }
    }
}
