using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с специализациями
/// </summary>
[ApiController]
[Route("api/specializations")]
public sealed class SpecializationController(ISpecializationService service) : ControllerBase
{
    /// <summary>
    /// Получить все специализации
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить все специализации")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll() => Ok((await service.SpecializationsAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить специализацию по идентификатору")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecializationDto>> Get(int id)
    {
        var entity = (await service.GetAsync(id, HttpContext.RequestAborted));
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать специализацию
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать специализацию")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpecializationDto>> Create(SaveSpecializationRequest request)
    {
        var entity = (await service.CreateAsync(request, HttpContext.RequestAborted));
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить специализацию")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecializationDto>> Update(int id, SaveSpecializationRequest request)
        => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить специализацию")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteSpecializationAsync(id, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по специализации
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/veterinarians")]
    [EndpointSummary("Получить ветеринаров по специализации")]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetVeterinarians(int id)
        => Ok((await service.VeterinariansBySpecializationAsync(id, HttpContext.RequestAborted)));
}
