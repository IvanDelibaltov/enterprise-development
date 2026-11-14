using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с приёмами
/// </summary>
[ApiController]
[Route("api/appointments")]
public sealed class AppointmentController(IAppointmentService service) : ControllerBase
{
    /// <summary>
    /// Получить все приёмы
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить все приёмы")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAll() => Ok((await service.AppointmentsAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить приём по идентификатору")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Get(int id)
    {
        var entity = (await service.GetAsync(id, HttpContext.RequestAborted));
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать приём
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать приём")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppointmentDto>> Create(SaveAppointmentRequest request)
    {
        var entity = (await service.CreateAsync(request, HttpContext.RequestAborted));
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить приём")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Update(int id, SaveAppointmentRequest request)
        => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить приём
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить приём")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAppointmentAsync(id, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Получить приёмы за текущий месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    [HttpGet("current-month")]
    [EndpointSummary("Получить приёмы за текущий месяц")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> CurrentMonth([FromQuery] int roomNumber)
    {
        if (roomNumber <= 0)
            return BadRequest();

        return Ok((await service.AppointmentsThisMonthAsync(roomNumber, DateTime.Today, HttpContext.RequestAborted)));
    }
}
