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
    [EndpointSummary("Get all owners")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OwnerDto>> GetAll() => Ok(service.Owners());

    /// <summary>
    /// Получить владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get owner by ID")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OwnerDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать владельца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create owner")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OwnerDto> Create(SaveOwnerRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update owner")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OwnerDto> Update(int id, SaveOwnerRequest request) => Ok(service.Update(id, request));

    /// <summary>
    /// Удалить владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete owner")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteOwner(id);
        return NoContent();
    }

    /// <summary>
    /// Получить владельцев нескольких питомцев
    /// </summary>
    [HttpGet("multiple-pets")]
    [EndpointSummary("Get owners with multiple pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OwnerDto>> MultiplePets() => Ok(service.OwnersWithMultiplePets());

    /// <summary>
    /// Получить питомцев владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by owner")]
    public ActionResult<IReadOnlyList<PetDto>> GetPets(int id) => Ok(service.PetsByOwner(id));
}
