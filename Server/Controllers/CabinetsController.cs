using DB;
using DB.Models.Departments;
using DB.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CabinetsController(ApplicationDBContext dbContext) : MainController(dbContext)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cabinet>>> GetCabinets()
    {
        return await DbContext.Cabinets.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddCabinet([FromBody] Cabinet cabinet)
    {
        if (cabinet is null)
            return BadRequest("Данные кабинета не предоставлены.");

        if (cabinet.Department is not null)
        {
            var existingDepartment = await DbContext.Departments.FirstOrDefaultAsync(d => d.Id == cabinet.Department.Id, CancellationToken);

            if (existingDepartment is null)
                DbContext.Departments.Add(cabinet.Department);
            else
                cabinet.Department = existingDepartment;
        }

        await DbContext.Cabinets.AddAsync(cabinet, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(cabinet);
    }
}
