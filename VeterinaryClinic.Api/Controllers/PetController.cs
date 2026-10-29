using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

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
    [EndpointSummary("Get all pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<PetDto>> GetAll() => Ok(service.Pets());
    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get pet by ID")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PetDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }
    /// <summary>
    /// Создать питомца
    /// </summary>
    [HttpPost]
    [EndpointSummary("Create pet")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PetDto> Create(SavePetRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    /// <summary>
    /// Обновить питомца
    /// </summary>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update pet")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PetDto> Update(int id, SavePetRequest request) => Ok(service.Update(id, request));
    /// <summary>
    /// Удалить питомца
    /// </summary>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete pet")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeletePet(id);
        return NoContent();
    }
    /// <summary>Возвращает приёмы питомца</summary>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Get appointments by pet")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<AppointmentDto>> GetAppointments(int id) =>
        Ok(service.AppointmentsByPet(id));

    /// <summary>Возвращает владельца питомца</summary>
    [HttpGet("{id:int}/owner")]
    [EndpointSummary("Get pet owner")]
    public ActionResult<OwnerDto> GetOwner(int id)
    {
        var owner = service.OwnerByPet(id);
        return owner is null ? NotFound() : Ok(owner);
    }
}
