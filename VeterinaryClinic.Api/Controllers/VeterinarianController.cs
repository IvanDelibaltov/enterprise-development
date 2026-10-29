using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;
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
    public ActionResult<IReadOnlyList<VeterinarianDto>> GetAll() => Ok(service.Veterinarians());
    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get veterinarian by ID")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VeterinarianDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }
    /// <summary>
    /// Создать ветеринара
    /// </summary>
    [HttpPost]
    [EndpointSummary("Create veterinarian")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VeterinarianDto> Create(SaveVeterinarianRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    /// <summary>
    /// Обновить ветеринара
    /// </summary>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update veterinarian")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VeterinarianDto> Update(int id, SaveVeterinarianRequest request)
        => Ok(service.Update(id, request));
    /// <summary>
    /// Удалить ветеринара
    /// </summary>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete veterinarian")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteVeterinarian(id);
        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по виду животного
    /// </summary>
    [HttpGet("by-species/{species}")]
    [EndpointSummary("Get veterinarians by animal species")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VeterinarianDto>> BySpecies(AnimalType species)
        => Ok(service.VeterinariansBySpecies(species));
    /// <summary>
    /// Получить приёмы ветеринара
    /// </summary>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Get appointments by veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<AppointmentDto>> GetAppointments(int id) =>
        Ok(service.AppointmentsByVeterinarian(id));

    /// <summary>Возвращает питомцев ветеринара без повторов</summary>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<PetDto>> PetsByDoctor(int id) => service.Get(id) is not null
        ? Ok(service.PetsByVeterinarian(id)) : NotFound();

}
