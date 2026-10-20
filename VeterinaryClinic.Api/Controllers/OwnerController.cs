using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts;
using VeterinaryClinic.Application.Services;

namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/owners")]
public sealed class OwnerController(OwnerService clinic) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all owners")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OwnerDto>> GetAll() => Ok(clinic.Owners());
    [HttpGet("{id:int}")]
    [EndpointSummary("Get owner by ID")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OwnerDto> Get(int id)
    {
        var entity = clinic.Owners().FirstOrDefault(x => x.Id == id);
        return entity is null ? NotFound() : Ok(entity);
    }
    [HttpPost]
    [EndpointSummary("Create owner")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OwnerDto> Create(SaveOwnerRequest request)
    {
        var entity = clinic.Save((int?)null, request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    [HttpPut("{id:int}")]
    [EndpointSummary("Update owner")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OwnerDto> Update(int id, SaveOwnerRequest request) => Ok(clinic.Save((int?)id, request));
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete owner")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        clinic.DeleteOwner(id);
        return NoContent();
    }

    [HttpGet("multiple-pets")]
    [EndpointSummary("Get owners with multiple pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OwnerDto>> MultiplePets() => Ok(clinic.OwnersWithMultiplePets());

}
