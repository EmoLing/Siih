using AutoMapper;
using Core.Models.Users;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Users;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobTitlesController(JobTitleService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<JobTitleObject>> GetJobTitle(int id)
    {
        var jobTitle = await service.GetJobTitleByIdAsync(id);

        if (jobTitle is null)
            BadRequest(id);

        var foundJobTitle = mapper.Map<JobTitleObject>(jobTitle);

        return Ok(foundJobTitle);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobTitleObject>>> GetJobTitles()
    {
        var coreJobTitles = await service.GetAllJobTitlesAsync();
        var serverJobTitles = mapper.Map<IEnumerable<JobTitleObject>>(coreJobTitles);
        return Ok(serverJobTitles);
    }

    [HttpPost]
    public async Task<ActionResult<JobTitleObject>> AddJobTitle([FromBody] JobTitleObject jobTitle)
    {
        if (jobTitle is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreJobTitle = mapper.Map<JobTitle>(jobTitle);
        var createdCoreJobTitle = await service.AddJobTitleAsync(coreJobTitle);
        var createdJobTitle = mapper.Map<JobTitleObject>(coreJobTitle);

        return CreatedAtAction(nameof(GetJobTitle), new { id = createdJobTitle.Id }, createdJobTitle);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateJobTitle(JobTitleObject jobTitle, CancellationToken cancellationToken)
    {
        if (jobTitle is null)
            return BadRequest();

        var coreJobTitle = mapper.Map<JobTitle>(jobTitle);
        await service.UpdateJobTitleAsync(coreJobTitle, cancellationToken);

        return NoContent();
    }

    [HttpDelete(Name = nameof(DeleteJobTitle))]
    public async Task<IActionResult> DeleteJobTitle(int id)
    {
        bool result = await service.DeleteJobTitleAsync(id);
        return result ? NoContent() : BadRequest();
    }
}
