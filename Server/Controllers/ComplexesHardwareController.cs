using AutoMapper;
using Core.Models.Equipment;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Equipment;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComplexesHardwareController(ComplexHardwareService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ComplexHardwareObject>> GetComplexHardware(int id)
    {
        var complexHardware = await service.GetComplexHardwareByIdAsync(id);

        if (complexHardware is null)
            BadRequest(id);

        var foundComplexHardware = mapper.Map<ComplexHardwareObject>(complexHardware);

        return Ok(foundComplexHardware);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplexHardwareObject>>> GetAllComplexesHardware()
    {
        var coreComplexesHardware = await service.GetAllComplexesHardwareAsync();
        var complexesHardware = mapper.Map<IEnumerable<ComplexHardwareObject>>(coreComplexesHardware);
        return Ok(complexesHardware);
    }

    [HttpPost]
    public async Task<ActionResult<ComplexHardwareObject>> AddComplexHardware([FromBody] ComplexHardwareObject complexHardware)
    {
        if (complexHardware is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreComplexHardware = mapper.Map<ComplexHardware>(complexHardware);
        var createdCoreComplexHardware = await service.AddComplexHardwareAsync(coreComplexHardware);
        var createdComplexHardware = mapper.Map<ComplexHardwareObject>(coreComplexHardware);

        return CreatedAtAction(nameof(GetComplexHardware), new { id = createdComplexHardware.Id }, createdComplexHardware);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComplexHardware(ComplexHardwareObject complexHardwareObject, CancellationToken cancellationToken)
    {
        if (complexHardwareObject is null)
            return BadRequest();

        var coreComplexHardware = mapper.Map<ComplexHardware>(complexHardwareObject);
        await service.UpdateComplexHardwareAsync(coreComplexHardware, cancellationToken);

        return NoContent();
    }

    [HttpDelete(Name = nameof(DeleteComplexHardware))]
    public async Task<IActionResult> DeleteComplexHardware(int id)
    {
        bool result = await service.DeleteComplexHardwareAsync(id);
        return result ? NoContent() : BadRequest();
    }
}