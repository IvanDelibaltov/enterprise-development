using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
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
    [EndpointSummary("Get all veterinarians")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetAll() => Ok((await service.VeterinariansAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get veterinarian by ID")]
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
    [EndpointSummary("Create veterinarian")]
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
    [EndpointSummary("Update veterinarian")]
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
    [EndpointSummary("Delete veterinarian")]
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
    [EndpointSummary("Get veterinarians by animal species")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> BySpecies(AnimalType species)
        => Ok((await service.VeterinariansBySpeciesAsync(species, HttpContext.RequestAborted)));

    /// <summary>
    /// Получить приёмы ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Get appointments by veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id) =>
        Ok((await service.AppointmentsByVeterinarianAsync(id, HttpContext.RequestAborted)));

    /// <summary>
    /// Возвращает питомцев ветеринара без повторов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> PetsByDoctor(int id) => (await service.GetAsync(id, HttpContext.RequestAborted)) is not null
        ? Ok((await service.PetsByVeterinarianAsync(id, HttpContext.RequestAborted))) : NotFound();
}
