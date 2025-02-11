using DB;
using DB.Models.Equipment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SoftwaresController(ApplicationDBContext dbContext) : MainController(dbContext)
{
    [HttpGet("{id}")]
    public async Task<Software> GetSoftware(int id) => await DbContext.Softwares.FirstOrDefaultAsync(s => s.Id == id, CancellationToken);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Software>>> GetSoftwares()
    {
        return await DbContext.Softwares.Include(s => s.Hardwares).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddSoftware([FromBody] Software software)
    {
        await DbContext.Softwares.AddAsync(software, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(software);
    }

    [HttpPatch(Name = nameof(UpdateSoftware))]
    public async Task<IActionResult> UpdateSoftware(Software software)
    {
        DbContext.Softwares.Update(software);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(software);
    }

    [HttpDelete(Name = nameof(DeleteSoftware))]
    public async Task<IActionResult> DeleteSoftware(int id)
    {
        var software = DbContext.Softwares.Find(id);

        if (software is null)
            return NotFound(id);

        DbContext.Softwares.Remove(software);
        await DbContext.SaveChangesAsync();

        return Ok(software);
    }
}
