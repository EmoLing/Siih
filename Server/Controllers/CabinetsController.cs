using DB;
using DB.Models.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CabinetsController(ApplicationDBContext dbContext) : MainController(dbContext)
{

    [HttpGet("{id}")]
    public async Task<Cabinet> GetCabinet(int id) => await DbContext.Cabinets.FirstOrDefaultAsync(u => u.Id == id, CancellationToken);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cabinet>>> GetCabinets()
    {
        return await DbContext.Cabinets.ToListAsync();
    }

    [HttpPost(Name = nameof(CreateCabinet))]
    public async Task<IActionResult> CreateCabinet(Cabinet cabinet)
    {
        await DbContext.Cabinets.AddAsync(cabinet, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(cabinet);
    }

    [HttpPatch(Name = nameof(UpdateCabinet))]
    public async Task<IActionResult> UpdateCabinet(Cabinet cabinet)
    {
        DbContext.Cabinets.Update(cabinet);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(cabinet);
    }

    [HttpDelete(Name = nameof(DeleteCabinet))]
    public async Task<IActionResult> DeleteCabinet(int id)
    {
        var cabinet = DbContext.Cabinets.Find(id);

        if (cabinet is null)
            return NotFound(id);

        DbContext.Cabinets.Remove(cabinet);
        await DbContext.SaveChangesAsync();

        return Ok(cabinet);
    }
}
