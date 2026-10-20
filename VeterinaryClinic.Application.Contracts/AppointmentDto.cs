using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

public record AppointmentDto(int Id, int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber, bool IsRepeated);
public record SaveAppointmentRequest(int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber, bool IsRepeated);
