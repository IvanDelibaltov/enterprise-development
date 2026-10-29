using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Repositories;

/// <summary>Операции с данными приёмов</summary>
public interface IAppointmentRepository
{
    /// <summary>Создаёт приём</summary>
    Appointment CreateAppointment(AppointmentCommand request);
    /// <summary>Обновляет приём</summary>
    Appointment UpdateAppointment(int id, AppointmentCommand request);
    /// <summary>Удаляет приём</summary>
    void DeleteAppointment(int id);
    /// <summary>Возвращает приёмы за текущий месяц в выбранном кабинете</summary>
    IReadOnlyList<Appointment> AppointmentsThisMonth(int room, DateTime today);
}
