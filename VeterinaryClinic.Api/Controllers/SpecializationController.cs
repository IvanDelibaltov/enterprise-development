using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/specializations")]
public sealed class SpecializationController(SpecializationService clinic) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all specializations")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll() => Ok(clinic.Specializations());
    [HttpGet("{id:int}")]
    [EndpointSummary("Get specialization by ID")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Get(int id)
    {
        var entity = clinic.Specializations().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    [HttpPost]
    [EndpointSummary("Create specialization")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<SpecializationDto> Create(SaveSpecializationRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    [HttpPut("{id:int}")]
    [EndpointSummary("Update specialization")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Update(int id, SaveSpecializationRequest request) => Ok(clinic.Save((int?)id, request));
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete specialization")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeleteSpecialization(id);
        return NoContent();
    }

}
