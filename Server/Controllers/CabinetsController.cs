using AutoMapper;
using Core.Models.Departments;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Departments;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CabinetsController(CabinetService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CabinetObject>> GetCabinet(int id)
    {
        var cabinet = await service.GetCabinetByIdAsync(id);

        if (cabinet is null)
            BadRequest(id);

        var foundCabinet = mapper.Map<CabinetObject>(cabinet);

        return Ok(foundCabinet);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CabinetObject>>> GetCabinets()
    {
        var coreCabinets = await service.GetAllCabinetsAsync();
        var serverCabinets = mapper.Map<IEnumerable<CabinetObject>>(coreCabinets);
        return Ok(serverCabinets);
    }

    [HttpPost]
    public async Task<ActionResult<CabinetObject>> AddCabinet([FromBody] CabinetObject cabinetObject)
    {
        if (cabinetObject is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreCabinet = mapper.Map<Cabinet>(cabinetObject);
        var createdCoreCabinet = await service.AddCabinetAsync(coreCabinet);
        var createdCabinet = mapper.Map<CabinetObject>(coreCabinet);

        return CreatedAtAction(nameof(GetCabinet), new { id = createdCabinet.Id }, createdCabinet);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCabinet(CabinetObject cabinetObject, CancellationToken cancellationToken)
    {
        if (cabinetObject is null)
            return BadRequest();

        var coreCabinet = mapper.Map<Cabinet>(cabinetObject);
        await service.UpdateCabinetAsync(coreCabinet, cancellationToken);

        return NoContent();
    }

    [HttpDelete(Name = nameof(DeleteCabinet))]
    public async Task<IActionResult> DeleteCabinet(int id)
    {
        bool result = await service.DeleteCabinetAsync(id);
        return result ? NoContent() : BadRequest();
    }
}
