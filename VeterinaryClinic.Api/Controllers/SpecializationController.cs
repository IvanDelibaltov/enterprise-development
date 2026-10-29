using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

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
    [EndpointSummary("Get all specializations")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll() => Ok(service.Specializations());
    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get specialization by ID")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }
    /// <summary>
    /// Создать специализацию
    /// </summary>
    [HttpPost]
    [EndpointSummary("Create specialization")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<SpecializationDto> Create(SaveSpecializationRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    /// <summary>
    /// Обновить специализацию
    /// </summary>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update specialization")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Update(int id, SaveSpecializationRequest request)
        => Ok(service.Update(id, request));
    /// <summary>
    /// Удалить специализацию
    /// </summary>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete specialization")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteSpecialization(id);
        return NoContent();
    }
    /// <summary>
    /// Получить ветеринаров по специализации
    /// </summary>
    [HttpGet("{id:int}/veterinarians")]
    [EndpointSummary("Get veterinarians by specialization")]
    public ActionResult<IReadOnlyList<VeterinarianDto>> GetVeterinarians(int id)
        => Ok(service.VeterinariansBySpecialization(id));
}
