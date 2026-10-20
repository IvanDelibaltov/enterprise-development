using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные записи на приём
/// </summary>
public record AppointmentDto(int Id, int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber, bool IsRepeated);
/// <summary>
/// Данные для создания или обновления записи на приём
/// </summary>
public record SaveAppointmentRequest(int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber, bool IsRepeated);
