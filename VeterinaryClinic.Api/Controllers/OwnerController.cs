using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с владельцами питомцев
/// </summary>
[ApiController]
[Route("api/owners")]
public sealed class OwnerController(OwnerService clinic) : ControllerBase
{
    /// <summary>
    /// Возвращает всех владельцев
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all owners")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OwnerDto>> GetAll() => Ok(clinic.Owners());
    /// <summary>
    /// Возвращает владельца по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get owner by ID")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OwnerDto> Get(int id)
    {
        var entity = clinic.Owners().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    /// <summary>
    /// Создаёт нового владельца
    /// </summary>
    [HttpPost]
    [EndpointSummary("Create owner")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OwnerDto> Create(SaveOwnerRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    /// <summary>
    /// Обновляет владельца по идентификатору
    /// </summary>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update owner")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OwnerDto> Update(int id, SaveOwnerRequest request) => Ok(clinic.Save((int?)id, request));
    /// <summary>
    /// Удаляет владельца по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete owner")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeleteOwner(id);
        return NoContent();
    }

    /// <summary>
    /// Возвращает владельцев с несколькими питомцами
    /// </summary>
    [HttpGet("multiple-pets")]
    [EndpointSummary("Get owners with multiple pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OwnerDto>> MultiplePets() => Ok(clinic.OwnersWithMultiplePets());

}
