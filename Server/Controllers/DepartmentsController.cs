using DB.Models.Departments;
using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB.Models.Equipment;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController(ApplicationDBContext dbContext) : MainController(dbContext)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await DbContext.Departments.Include(d => d.Cabinets).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddDepartment([FromBody] Department department)
    {
        if (department is null)
            return BadRequest("Данные кабинета не предоставлены.");

        if (department.Cabinets.Count > 0)
        {
            var existingCabinets = await DbContext.Cabinets.Where(c => department.Cabinets.Contains(c)).ToListAsync();
            var newExistingCabinets = department.Cabinets.Where(c => !existingCabinets.Contains(c));

            department.Cabinets.Clear();

            if (newExistingCabinets.Any())
                await DbContext.Cabinets.AddRangeAsync(newExistingCabinets);

            if (existingCabinets.Count > 0)
                department.Cabinets.AddRange(existingCabinets);
        }

        await DbContext.Departments.AddAsync(department, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(department);
    }
}
