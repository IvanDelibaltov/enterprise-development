using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/pets")]
public sealed class PetController(PetService clinic) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<PetDto>> GetAll() => Ok(clinic.Pets());
    [HttpGet("{id:int}")]
    [EndpointSummary("Get pet by ID")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PetDto> Get(int id)
    {
        var entity = clinic.Pets().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    [HttpPost]
    [EndpointSummary("Create pet")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PetDto> Create(SavePetRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    [HttpPut("{id:int}")]
    [EndpointSummary("Update pet")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PetDto> Update(int id, SavePetRequest request) => Ok(clinic.Save((int?)id, request));
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete pet")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeletePet(id);
        return NoContent();
    }

}
