using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с питомцами
/// </summary>
[ApiController]
[Route("api/pets")]
public sealed class PetController(IPetService service) : ControllerBase
{
    /// <summary>
    /// Получить всех питомцев
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить всех питомцев")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetAll() => Ok((await service.PetsAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить питомца по идентификатору")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Get(int id)
    {
        var entity = (await service.GetAsync(id, HttpContext.RequestAborted));
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать питомца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать питомца")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PetDto>> Create(SavePetRequest request)
    {
        var entity = (await service.CreateAsync(request, HttpContext.RequestAborted));
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить питомца")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Update(int id, SavePetRequest request) => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить питомца")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeletePetAsync(id, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Возвращает приёмы питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Получить приёмы питомца")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id) =>
        Ok((await service.AppointmentsByPetAsync(id, HttpContext.RequestAborted)));

    /// <summary>
    /// Возвращает владельца питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/owner")]
    [EndpointSummary("Получить владельца питомца")]
    public async Task<ActionResult<OwnerDto>> GetOwner(int id)
    {
        var owner = (await service.OwnerByPetAsync(id, HttpContext.RequestAborted));
        return owner is null ? NotFound() : Ok(owner);
    }
}
