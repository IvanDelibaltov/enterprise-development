namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные для создания или обновления приёма
/// </summary>
/// <param name="PetId">Идентификатор питомца</param>
/// <param name="VeterinarianId">Идентификатор ветеринара</param>
/// <param name="AppointmentDate">Дата и время приёма</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeated">Признак повторного приёма</param>
public record SaveAppointmentRequest(int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber,
    bool IsRepeated);
