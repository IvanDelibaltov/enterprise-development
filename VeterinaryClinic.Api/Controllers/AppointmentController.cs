using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с записями на приём
/// </summary>
[ApiController]
[Route("api/appointments")]
public sealed class AppointmentController(AppointmentService clinic) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи на приём
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<AppointmentDto>> GetAll() => Ok(clinic.Appointments());
    /// <summary>
    /// Возвращает запись на приём по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get appointment by ID")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AppointmentDto> Get(int id)
    {
        var entity = clinic.Appointments().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    /// <summary>
    /// Создаёт новую запись на приём
    /// </summary>
    [HttpPost]
    [EndpointSummary("Create appointment")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AppointmentDto> Create(SaveAppointmentRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    /// <summary>
    /// Обновляет запись на приём по идентификатору
    /// </summary>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update appointment")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AppointmentDto> Update(int id, SaveAppointmentRequest request) => Ok(clinic.Save((int?)id, request));
    /// <summary>
    /// Удаляет запись на приём по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete appointment")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeleteAppointment(id);
        return NoContent();
    }

    /// <summary>
    /// Возвращает записи на приём за текущий месяц
    /// </summary>
    [HttpGet("current-month")]
    [EndpointSummary("Get appointments for the current month")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<AppointmentDto>> CurrentMonth([FromQuery] int roomNumber) => roomNumber > 0 ? Ok(clinic.AppointmentsThisMonth(roomNumber, DateTime.Today)) : BadRequest();

}
