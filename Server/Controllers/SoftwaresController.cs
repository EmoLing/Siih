using AutoMapper;
using Core.Models.Equipment;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Equipment;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SoftwaresController(SoftwareService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<SoftwareObject>> GetSoftware(int id)
    {
        var software = await service.GetSoftwareByIdAsync(id);

        if (software is null)
            BadRequest(id);

        var foundSoftware = mapper.Map<SoftwareObject>(software);

        return Ok(foundSoftware);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SoftwareObject>>> GetSoftwares()
    {
        var coreSoftwares = await service.GetAllSoftwaresAsync();
        var serverSoftwares = mapper.Map<IEnumerable<SoftwareObject>>(coreSoftwares);
        return Ok(serverSoftwares);
    }

    [HttpPost]
    public async Task<ActionResult<SoftwareObject>> AddSoftware([FromBody] SoftwareObject software)
    {
        if (software is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreSoftware = mapper.Map<Software>(software);
        var createdCoreSoftware = await service.AddSoftwareAsync(coreSoftware);
        var createdSoftware = mapper.Map<SoftwareObject>(coreSoftware);

        return CreatedAtAction(nameof(GetSoftware), new { id = createdSoftware.Id }, createdSoftware);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSoftware(SoftwareObject software, CancellationToken cancellationToken)
    {
        if (software is null)
            return BadRequest();

        var coreSoftware = mapper.Map<Software>(software);
        await service.UpdateSoftwareAsync(coreSoftware, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSoftware(int id)
    {
        bool result = await service.DeleteSoftwareAsync(id);
        return result ? Ok() : BadRequest();
    }
}
