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
    public ActionResult<IReadOnlyList<BreedDto>> GetAll() => Ok(service.Breeds());

    /// <summary>
    /// Получить породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get breed by ID")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BreedDto> Get(int id)
    {
        var entity = service.Get(id);
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
    public ActionResult<BreedDto> Create(SaveBreedRequest request)
    {
        var entity = service.Create(request);
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
    public ActionResult<BreedDto> Update(int id, SaveBreedRequest request) => Ok(service.Update(id, request));

    /// <summary>
    /// Удалить породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete breed")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteBreed(id);
        return NoContent();
    }

    /// <summary>
    /// Посчитать повторные приёмы по породе
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/repeated-appointments/count")]
    [EndpointSummary("Count repeated appointments by breed")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public ActionResult<int> RepeatedCount(int id) => Ok(service.RepeatedAppointments(id));

    /// <summary>
    /// Получить питомцев по породе
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by breed")]
    public ActionResult<IReadOnlyList<PetDto>> GetPets(int id) => Ok(service.PetsByBreed(id));
}
