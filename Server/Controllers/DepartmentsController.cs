using AutoMapper;
using Core.Models.Departments;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Departments;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController(DepartmentService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentObject>> GetDepartment(int id)
    {
        var department = await service.GetDepartmentByIdAsync(id);

        if (department is null)
            BadRequest(id);

        var foundDepartment = mapper.Map<DepartmentObject>(department);

        return Ok(foundDepartment);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentObject>>> GetAllDepartments()
    {
        var coreDepartments = await service.GetAllDepartmentsAsync();
        var serverDepartment = mapper.Map<IEnumerable<DepartmentObject>>(coreDepartments);
        return Ok(serverDepartment);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentObject>> AddDepartment([FromBody] DepartmentObject departmentObject)
    {
        if (departmentObject is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreDepartment = mapper.Map<Department>(departmentObject);
        var createdCoreDepartment = await service.AddDepartmentAsync(coreDepartment);
        var createdDepartment = mapper.Map<DepartmentObject>(coreDepartment);

        return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.Id }, createdDepartment);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDepartment(DepartmentObject departmentObject, CancellationToken cancellationToken)
    {
        if (departmentObject is null)
            return BadRequest();

        var coreDepartment = mapper.Map<Department>(departmentObject);
        await service.UpdateDepartmentAsync(coreDepartment, cancellationToken);

        return NoContent();
    }

    [HttpDelete(Name = nameof(DeleteDepartment))]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        bool result = await service.DeleteDepartmentAsync(id);
        return result ? NoContent() : BadRequest();
    }
}

