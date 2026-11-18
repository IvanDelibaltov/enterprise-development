using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
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
    [EndpointSummary("Get all pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetAll() => Ok((await service.PetsAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get pet by ID")]
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
    [EndpointSummary("Create pet")]
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
    [EndpointSummary("Update pet")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Update(int id, SavePetRequest request) => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete pet")]
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
    [EndpointSummary("Get appointments by pet")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id) =>
        Ok((await service.AppointmentsByPetAsync(id, HttpContext.RequestAborted)));

    /// <summary>
    /// Возвращает владельца питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/owner")]
    [EndpointSummary("Get owner of pet")]
    public async Task<ActionResult<OwnerDto>> GetOwner(int id)
    {
        var owner = (await service.OwnerByPetAsync(id, HttpContext.RequestAborted));
        return owner is null ? NotFound() : Ok(owner);
    }
}
