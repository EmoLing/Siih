using DB;
using DB.Models.Equipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HardwaresController(ApplicationDBContext dbContext) : MainController(dbContext)
{
    [HttpGet("{id}")]
    public async Task<Hardware> GetSoftware(int id) => await DbContext.Hardwares.FirstOrDefaultAsync(h => h.Id == id, CancellationToken);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hardware>>> GetSoftwares()
    {
        return await DbContext.Hardwares.Include(h => h.Softwares).Include(h => h.ComplexHardware).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddHardware([FromBody] Hardware hardware)
    {
        await DbContext.Hardwares.AddAsync(hardware, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(hardware);
    }

    [HttpPatch(Name = nameof(UpdateHardware))]
    public async Task<IActionResult> UpdateHardware(Hardware hardware)
    {
        DbContext.Hardwares.Update(hardware);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(hardware);
    }

    [HttpDelete(Name = nameof(DeleteHardware))]
    public async Task<IActionResult> DeleteHardware(int id)
    {
        var hardware = DbContext.Hardwares.Find(id);

        if (hardware is null)
            return NotFound(id);

        DbContext.Hardwares.Remove(hardware);
        await DbContext.SaveChangesAsync();

        return Ok(hardware);
    }
}
