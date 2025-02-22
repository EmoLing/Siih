using AutoMapper;
using Core.Models.Equipment;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Equipment;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HardwaresController(HardwareService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<HardwareObject>> GetHardware(int id)
    {
        var hardware = await service.GetHardwareByIdAsync(id);

        if (hardware is null)
            BadRequest(id);

        var foundHardware = mapper.Map<HardwareObject>(hardware);

        return Ok(foundHardware);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HardwareObject>>> GetHardwares()
    {
        var coreHardwares = await service.GetAllHardwaresAsync();
        var serverHardware = mapper.Map<IEnumerable<HardwareObject>>(coreHardwares);
        return Ok(serverHardware);
    }

    [HttpPost]
    public async Task<ActionResult<HardwareObject>> AddHardware([FromBody] HardwareObject hardwareObject)
    {
        if (hardwareObject is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreHardware = mapper.Map<Hardware>(hardwareObject);
        var createdCoreHardware = await service.AddHardwareAsync(coreHardware);
        var createdHardware = mapper.Map<HardwareObject>(coreHardware);

        return CreatedAtAction(nameof(GetHardware), new { id = createdHardware.Id }, createdHardware);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateHardware(HardwareObject hardwareObject, CancellationToken cancellationToken)
    {
        if (hardwareObject is null)
            return BadRequest();

        var coreHardware = mapper.Map<Hardware>(hardwareObject);
        await service.UpdateHardwareAsync(coreHardware, cancellationToken);

        return NoContent();
    }

    [HttpDelete(Name = nameof(DeleteHardware))]
    public async Task<IActionResult> DeleteHardware(int id)
    {
        bool result = await service.DeleteHardwareAsync(id);
        return result ? NoContent() : BadRequest();
    }
}
