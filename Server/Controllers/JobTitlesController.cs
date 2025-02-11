using DB;
using DB.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobTitlesController(ApplicationDBContext dbContext) : MainController(dbContext)
{

    [HttpGet("{id}")]
    public async Task<JobTitle> GetJobTitle(int id) => await DbContext.JobTitles.FirstOrDefaultAsync(u => u.Id == id, CancellationToken);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobTitle>>> GetJobTitles()
    {
        return await DbContext.JobTitles.ToListAsync();
    }

    [HttpPost(Name = nameof(CreateJobTitle))]
    public async Task<IActionResult> CreateJobTitle(JobTitle jobTitle)
    {
        await DbContext.JobTitles.AddAsync(jobTitle, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(jobTitle);
    }

    [HttpPatch(Name = nameof(UpdateJobTitle))]
    public async Task<IActionResult> UpdateJobTitle(JobTitle jobTitle)
    {
        DbContext.JobTitles.Update(jobTitle);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(jobTitle);
    }

    [HttpDelete(Name = nameof(DeleteJobTitle))]
    public async Task<IActionResult> DeleteJobTitle(int id)
    {
        var jobTitle = DbContext.JobTitles.Find(id);

        if (jobTitle is null)
            return NotFound(id);

        DbContext.JobTitles.Remove(jobTitle);
        await DbContext.SaveChangesAsync();

        return Ok(jobTitle);
    }
}
