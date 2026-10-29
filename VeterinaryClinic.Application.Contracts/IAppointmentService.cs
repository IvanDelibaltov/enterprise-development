namespace VeterinaryClinic.Application.Contracts;

/// <summary>Операции сервиса приёмов</summary>
public interface IAppointmentService
{
    /// <summary>Возвращает все приёмы</summary>
    IReadOnlyList<AppointmentDto> Appointments();
    /// <summary>Возвращает приём по идентификатору</summary>
    AppointmentDto? Get(int id);
    /// <summary>Создаёт приём</summary>
    AppointmentDto Create(SaveAppointmentRequest request);
    /// <summary>Обновляет приём по идентификатору</summary>
    AppointmentDto Update(int id, SaveAppointmentRequest request);
    /// <summary>Удаляет приём по идентификатору</summary>
    void DeleteAppointment(int id);
    /// <summary>Возвращает приёмы за месяц по кабинету</summary>
    IReadOnlyList<AppointmentDto> AppointmentsThisMonth(int room, DateTime today);
}
