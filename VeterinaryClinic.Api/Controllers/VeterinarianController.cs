using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с ветеринарами
/// </summary>
[ApiController]
[Route("api/veterinarians")]
public sealed class VeterinarianController(IVeterinarianService service) : ControllerBase
{
    /// <summary>
    /// Получить всех ветеринаров
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить всех ветеринаров")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetAll() => Ok((await service.VeterinariansAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить ветеринара по идентификатору")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeterinarianDto>> Get(int id)
    {
        var entity = (await service.GetAsync(id, HttpContext.RequestAborted));
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать ветеринара
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать ветеринара")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeterinarianDto>> Create(SaveVeterinarianRequest request)
    {
        var entity = (await service.CreateAsync(request, HttpContext.RequestAborted));
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить ветеринара")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeterinarianDto>> Update(int id, SaveVeterinarianRequest request)
        => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить ветеринара")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteVeterinarianAsync(id, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по виду животного
    /// </summary>
    /// <param name="species">Вид животного</param>
    [HttpGet("by-species/{species}")]
    [EndpointSummary("Получить ветеринаров по виду животного")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> BySpecies(AnimalType species)
        => Ok((await service.VeterinariansBySpeciesAsync(species, HttpContext.RequestAborted)));

    /// <summary>
    /// Получить приёмы ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Получить приёмы ветеринара")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id) =>
        Ok((await service.AppointmentsByVeterinarianAsync(id, HttpContext.RequestAborted)));

    /// <summary>
    /// Возвращает питомцев ветеринара без повторов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Получить питомцев ветеринара")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> PetsByDoctor(int id) => (await service.GetAsync(id, HttpContext.RequestAborted)) is not null
        ? Ok((await service.PetsByVeterinarianAsync(id, HttpContext.RequestAborted))) : NotFound();
}
