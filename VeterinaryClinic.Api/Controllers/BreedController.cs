using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/breeds")]
public sealed class BreedController(BreedService clinic) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all breeds")]
    [ProducesResponseType(typeof(IReadOnlyList<BreedDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<BreedDto>> GetAll() => Ok(clinic.Breeds());
    [HttpGet("{id:int}")]
    [EndpointSummary("Get breed by ID")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BreedDto> Get(int id)
    {
        var entity = clinic.Breeds().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    [HttpPost]
    [EndpointSummary("Create breed")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<BreedDto> Create(SaveBreedRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    [HttpPut("{id:int}")]
    [EndpointSummary("Update breed")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BreedDto> Update(int id, SaveBreedRequest request) => Ok(clinic.Save((int?)id, request));
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete breed")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeleteBreed(id);
        return NoContent();
    }

    [HttpGet("{id:int}/repeated-appointments/count")]
    [EndpointSummary("Count repeated appointments by breed")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public ActionResult<int> RepeatedCount(int id) => clinic.Breeds().Any(x => x.Id == id) ? Ok(clinic.RepeatedAppointments(id)) : NotFound();

}
