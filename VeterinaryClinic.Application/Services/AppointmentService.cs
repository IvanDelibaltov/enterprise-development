using VeterinaryClinic.Domain.Commands;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Сервис приёмов
/// </summary>
public sealed class AppointmentService(IRepository<Appointment> appointments,
    IAppointmentRepository repository) : IAppointmentService
{
    /// <summary>
    /// Возвращает все приёмы
    /// </summary>
    public IReadOnlyList<AppointmentDto> Appointments() => appointments.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Возвращает приёмы за текущий месяц в выбранном кабинете
    /// </summary>
    public IReadOnlyList<AppointmentDto> AppointmentsThisMonth(int room, DateTime today)
        => repository.AppointmentsThisMonth(room,
        today).Select(DtoMapper.Map).ToArray();

    /// <summary>Возвращает приём по идентификатору</summary>
    public AppointmentDto? Get(int id) => appointments.Get(id) is { } entity ? DtoMapper.Map(entity) : null;

    /// <summary>Создаёт приём</summary>
    public AppointmentDto Create(SaveAppointmentRequest x)
    {
        if (x.RoomNumber <= 0 || x.AppointmentDate == default) throw new ArgumentException("Invalid appointment");
        var saved = repository.CreateAppointment(new AppointmentCommand(x.PetId, x.VeterinarianId,
            x.AppointmentDate, x.RoomNumber, x.IsRepeated));
        return DtoMapper.Map(saved);
    }

    /// <summary>Обновляет приём</summary>
    public AppointmentDto Update(int id, SaveAppointmentRequest x)
    {
        if (x.RoomNumber <= 0 || x.AppointmentDate == default) throw new ArgumentException("Invalid appointment");
        var saved = repository.UpdateAppointment(id, new AppointmentCommand(x.PetId, x.VeterinarianId,
            x.AppointmentDate, x.RoomNumber, x.IsRepeated));
        return DtoMapper.Map(saved);
    }

    /// <summary>
    /// Удаляет приём
    /// </summary>
    public void DeleteAppointment(int id) => repository.DeleteAppointment(id);
}
