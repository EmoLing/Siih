using DB;
using DB.Models.Users;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ApplicationDBContext dbContext) : MainController(dbContext)
{
    [HttpGet("{id}")]
    public async Task<User> GetUser(int id) => await DbContext.Users.FirstOrDefaultAsync(u => u.Id == id, CancellationToken);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await DbContext.Users
            .Include(u => u.JobTitle)
            .Include(u => u.Cabinet)
            .Include(u => u.ComplexHardwares).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        if (user is null)
            return BadRequest("Данные пользователя не предоставлены.");

        if (user.JobTitle is not null)
        {
            var existingJobTitle = await DbContext.JobTitles.FirstOrDefaultAsync(jt => jt.Id == user.JobTitle.Id, CancellationToken);

            if (existingJobTitle is null)
                DbContext.JobTitles.Add(user.JobTitle);
            else
                user.JobTitle = existingJobTitle;
        }

        if (user.Cabinet is not null)
        {
            var existingCabinet = await DbContext.Cabinets.FirstOrDefaultAsync(c => c.Id == user.Cabinet.Id, CancellationToken);

            if (existingCabinet is null)
                DbContext.Cabinets.Add(user.Cabinet);
            else
                user.Cabinet = existingCabinet;
        }

        await DbContext.Users.AddAsync(user, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] JsonPatchDocument<User> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("Данные для обновления не предоставлены.");

        var existingUser = await DbContext.Users.FindAsync(id);

        if (existingUser is null)
            return NotFound(id);

        patchDoc.ApplyTo(existingUser, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(existingUser);
    }

    [HttpDelete(Name = nameof(DeleteUser))]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = DbContext.Users.Find(id);

        if (user is null)
            return NotFound(id);

        DbContext.Users.Remove(user);
        await DbContext.SaveChangesAsync();

        return Ok(user);
    }
}
