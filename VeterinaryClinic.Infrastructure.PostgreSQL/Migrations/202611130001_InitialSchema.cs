using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Infrastructure.PostgreSQL.Migrations;

[DbContext(typeof(ClinicDbContext))]
[Migration("202611130001_InitialSchema")]
public sealed class InitialSchema : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("breeds", columns: t => new
        {
            Id = t.Column<int>(nullable: false), Name = t.Column<string>(nullable: false), AnimalType = t.Column<string>(nullable: false)
        }, constraints: t => t.PrimaryKey("PK_breeds", x => x.Id));
        migrationBuilder.CreateTable("specializations", columns: t => new
        {
            Id = t.Column<int>(nullable: false), Name = t.Column<string>(nullable: false), AnimalType = t.Column<string>(nullable: false)
        }, constraints: t => t.PrimaryKey("PK_specializations", x => x.Id));
        migrationBuilder.CreateTable("owners", columns: t => new
        {
            Id = t.Column<int>(nullable: false), LastName = t.Column<string>(nullable: false), FirstName = t.Column<string>(nullable: false),
            Patronymic = t.Column<string>(nullable: true), Phone = t.Column<string>(nullable: false), Address = t.Column<string>(nullable: false)
        }, constraints: t => t.PrimaryKey("PK_owners", x => x.Id));
        migrationBuilder.CreateTable("veterinarians", columns: t => new
        {
            Id = t.Column<int>(nullable: false), LastName = t.Column<string>(nullable: false), FirstName = t.Column<string>(nullable: false),
            Patronymic = t.Column<string>(nullable: true), Phone = t.Column<string>(nullable: false), PassportNumber = t.Column<string>(nullable: false),
            BirthDate = t.Column<DateOnly>(type: "date", nullable: false), ExperienceYears = t.Column<int>(nullable: false),
            SpecializationId = t.Column<int>(nullable: false)
        }, constraints: t => { t.PrimaryKey("PK_veterinarians", x => x.Id); t.ForeignKey("FK_veterinarians_specializations_SpecializationId", x => x.SpecializationId, "specializations", "Id", onDelete: ReferentialAction.Restrict); });
        migrationBuilder.CreateTable("pets", columns: t => new
        {
            Id = t.Column<int>(nullable: false), Name = t.Column<string>(nullable: false), BirthDate = t.Column<DateOnly>(type: "date", nullable: false),
            Weight = t.Column<double>(nullable: false), BreedId = t.Column<int>(nullable: false), OwnerId = t.Column<int>(nullable: true)
        }, constraints: t => { t.PrimaryKey("PK_pets", x => x.Id); t.ForeignKey("FK_pets_breeds_BreedId", x => x.BreedId, "breeds", "Id", onDelete: ReferentialAction.Restrict); t.ForeignKey("FK_pets_owners_OwnerId", x => x.OwnerId, "owners", "Id", onDelete: ReferentialAction.Restrict); });
        migrationBuilder.CreateTable("appointments", columns: t => new
        {
            Id = t.Column<int>(nullable: false), AppointmentDate = t.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            RoomNumber = t.Column<int>(nullable: false), IsRepeated = t.Column<bool>(nullable: false),
            PetId = t.Column<int>(nullable: false), VeterinarianId = t.Column<int>(nullable: false)
        }, constraints: t => { t.PrimaryKey("PK_appointments", x => x.Id); t.ForeignKey("FK_appointments_pets_PetId", x => x.PetId, "pets", "Id", onDelete: ReferentialAction.Restrict); t.ForeignKey("FK_appointments_veterinarians_VeterinarianId", x => x.VeterinarianId, "veterinarians", "Id", onDelete: ReferentialAction.Restrict); });
        migrationBuilder.CreateIndex("IX_veterinarians_SpecializationId", "veterinarians", "SpecializationId");
        migrationBuilder.CreateIndex("IX_pets_BreedId", "pets", "BreedId");
        migrationBuilder.CreateIndex("IX_pets_OwnerId", "pets", "OwnerId");
        migrationBuilder.CreateIndex("IX_appointments_PetId", "appointments", "PetId");
        migrationBuilder.CreateIndex("IX_appointments_VeterinarianId", "appointments", "VeterinarianId");
        migrationBuilder.CreateIndex("IX_appointments_AppointmentDate_RoomNumber", "appointments", new[] { "AppointmentDate", "RoomNumber" }, unique: true);
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("appointments");
        migrationBuilder.DropTable("pets");
        migrationBuilder.DropTable("veterinarians");
        migrationBuilder.DropTable("owners");
        migrationBuilder.DropTable("breeds");
        migrationBuilder.DropTable("specializations");
    }
}
