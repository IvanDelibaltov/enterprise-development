using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с владельцами
/// </summary>
[ApiController]
[Route("api/owners")]
public sealed class OwnerController(IOwnerService service) : ControllerBase
{
    /// <summary>
    /// Получить всех владельцев
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить всех владельцев")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetAll() => Ok((await service.OwnersAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить владельца по идентификатору")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OwnerDto>> Get(int id)
    {
        var entity = (await service.GetAsync(id, HttpContext.RequestAborted));
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать владельца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать владельца")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OwnerDto>> Create(SaveOwnerRequest request)
    {
        var entity = (await service.CreateAsync(request, HttpContext.RequestAborted));
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить владельца")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OwnerDto>> Update(int id, SaveOwnerRequest request) => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить владельца")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteOwnerAsync(id, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Получить владельцев нескольких питомцев
    /// </summary>
    [HttpGet("multiple-pets")]
    [EndpointSummary("Получить владельцев нескольких питомцев")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> MultiplePets() => Ok((await service.OwnersWithMultiplePetsAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить питомцев владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Получить питомцев владельца")]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPets(int id) => Ok((await service.PetsByOwnerAsync(id, HttpContext.RequestAborted)));
}
