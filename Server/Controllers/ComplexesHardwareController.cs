using DB;
using DB.Models.Equipment;
using DB.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComplexesHardwareController(ApplicationDBContext dbContext) : MainController(dbContext)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplexHardware>>> GetComplexesHardware()
    {
        return await DbContext.ComplexesHardware.Include(ch => ch.Hardwares).Include(ch => ch.User).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddComplexHardware([FromBody] ComplexHardware complexHardware)
    {
        if (complexHardware.User is not null)
        {
            var existingUser = await DbContext.Users.FirstOrDefaultAsync(ch => ch.Id == complexHardware.User.Id, CancellationToken);

            if (existingUser is null)
                DbContext.Users.Add(complexHardware.User);
            else
                complexHardware.User = existingUser;
        }

        if (complexHardware.Hardwares.Count > 0)
        {
            var existingHardwares = await DbContext.Hardwares.Where(h => complexHardware.Hardwares.Contains(h)).ToListAsync();
            var newExistingHardwares = complexHardware.Hardwares.Where(h => !existingHardwares.Contains(h));

            complexHardware.Hardwares.Clear();

            if (newExistingHardwares.Any())
                await DbContext.Hardwares.AddRangeAsync(newExistingHardwares);

            if (existingHardwares.Any())
                complexHardware.Hardwares.AddRange(existingHardwares);
        }

        await DbContext.ComplexesHardware.AddAsync(complexHardware, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(complexHardware);
    }
}
