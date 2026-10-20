using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/veterinarians")]
public sealed class VeterinarianController(VeterinarianService clinic) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all veterinarians")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VeterinarianDto>> GetAll() => Ok(clinic.Veterinarians());
    [HttpGet("{id:int}")]
    [EndpointSummary("Get veterinarian by ID")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VeterinarianDto> Get(int id)
    {
        var entity = clinic.Veterinarians().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    [HttpPost]
    [EndpointSummary("Create veterinarian")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VeterinarianDto> Create(SaveVeterinarianRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    [HttpPut("{id:int}")]
    [EndpointSummary("Update veterinarian")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VeterinarianDto> Update(int id, SaveVeterinarianRequest request) => Ok(clinic.Save((int?)id, request));
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete veterinarian")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeleteVeterinarian(id);
        return NoContent();
    }

    [HttpGet("by-species/{species}")]
    [EndpointSummary("Get veterinarians by animal species")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VeterinarianDto>> BySpecies(AnimalType species) => Ok(clinic.VeterinariansBySpecies(species));
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets assigned to veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<PetDto>> PetsByDoctor(int id) => clinic.Veterinarians().Any(x => x.Id == id) ? Ok(clinic.PetsByVeterinarian(id)) : NotFound();

}
