using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с породами
/// </summary>
[ApiController]
[Route("api/breeds")]
public sealed class BreedController(IBreedService service) : ControllerBase
{
    /// <summary>
    /// Получить все породы
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all breeds")]
    [ProducesResponseType(typeof(IReadOnlyList<BreedDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BreedDto>>> GetAll() => Ok((await service.BreedsAsync(HttpContext.RequestAborted)));

    /// <summary>
    /// Получить породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get breed by ID")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Get(int id)
    {
        var entity = (await service.GetAsync(id, HttpContext.RequestAborted));
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать породу
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create breed")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BreedDto>> Create(SaveBreedRequest request)
    {
        var entity = (await service.CreateAsync(request, HttpContext.RequestAborted));
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update breed")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Update(int id, SaveBreedRequest request) => Ok((await service.UpdateAsync(id, request, HttpContext.RequestAborted)));

    /// <summary>
    /// Удалить породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete breed")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteBreedAsync(id, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Посчитать повторные приёмы по породе
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/repeated-appointments/count")]
    [EndpointSummary("Count repeated appointments by breed")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> RepeatedCount(int id) => Ok((await service.RepeatedAppointmentsAsync(id, HttpContext.RequestAborted)));

    /// <summary>
    /// Получить питомцев по породе
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by breed")]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPets(int id) => Ok((await service.PetsByBreedAsync(id, HttpContext.RequestAborted)));
}
