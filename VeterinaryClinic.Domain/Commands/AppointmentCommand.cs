using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Commands;

/// <summary>Данные для сохранения приёма</summary>
public sealed record AppointmentCommand(int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber,
    bool IsRepeated);
