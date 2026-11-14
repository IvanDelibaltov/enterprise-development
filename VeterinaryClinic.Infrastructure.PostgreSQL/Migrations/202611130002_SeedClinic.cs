using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using VeterinaryClinic.Domain.Data;

namespace VeterinaryClinic.Infrastructure.PostgreSQL.Migrations;

[DbContext(typeof(ClinicDbContext))]
[Migration("202611130002_SeedClinic")]
public sealed class SeedClinic : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        foreach (var x in VeterinaryClinicTestData.Breeds)
            migrationBuilder.InsertData(
                table: "breeds",
                columns: new[] { "Id", "Name", "AnimalType" },
                columnTypes: new[] { "integer", "text", "text" },
                values: new object[] { x.Id, x.Name, x.AnimalType.ToString() });

        foreach (var x in VeterinaryClinicTestData.Specializations)
            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "Id", "Name", "AnimalType" },
                columnTypes: new[] { "integer", "text", "text" },
                values: new object[] { x.Id, x.Name, x.AnimalType.ToString() });

        foreach (var x in VeterinaryClinicTestData.Owners)
            migrationBuilder.InsertData(
                table: "owners",
                columns: new[] { "Id", "LastName", "FirstName", "Patronymic", "Phone", "Address" },
                columnTypes: new[] { "integer", "text", "text", "text", "text", "text" },
                values: new object?[] { x.Id, x.LastName, x.FirstName, x.Patronymic, x.Phone, x.Address });

        foreach (var x in VeterinaryClinicTestData.Veterinarians)
            migrationBuilder.InsertData(
                table: "veterinarians",
                columns: new[] { "Id", "LastName", "FirstName", "Patronymic", "Phone", "PassportNumber", "BirthDate", "ExperienceYears", "SpecializationId" },
                columnTypes: new[] { "integer", "text", "text", "text", "text", "text", "date", "integer", "integer" },
                values: new object?[] { x.Id, x.LastName, x.FirstName, x.Patronymic, x.Phone, x.PassportNumber, x.BirthDate, x.ExperienceYears, x.Specialization.Id });

        foreach (var x in VeterinaryClinicTestData.Pets)
            migrationBuilder.InsertData(
                table: "pets",
                columns: new[] { "Id", "Name", "BirthDate", "Weight", "BreedId", "OwnerId" },
                columnTypes: new[] { "integer", "text", "date", "double precision", "integer", "integer" },
                values: new object?[] { x.Id, x.Name, x.BirthDate, x.Weight, x.Breed.Id, x.Owner?.Id });

        foreach (var x in VeterinaryClinicTestData.Appointments)
            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "Id", "AppointmentDate", "RoomNumber", "IsRepeated", "PetId", "VeterinarianId" },
                columnTypes: new[] { "integer", "timestamp without time zone", "integer", "boolean", "integer", "integer" },
                values: new object[] { x.Id, x.AppointmentDate, x.RoomNumber, x.IsRepeated, x.Pet.Id, x.Veterinarian.Id });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            "DELETE FROM appointments; DELETE FROM pets; DELETE FROM veterinarians; " +
            "DELETE FROM owners; DELETE FROM breeds; DELETE FROM specializations;");
    }
}
