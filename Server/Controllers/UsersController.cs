using DB;
using DB.Models.Users;
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
        return await DbContext.Users.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        if (user is null)
            return BadRequest("Данные пользователя не предоставлены.");

        await DbContext.Users.AddAsync(user, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(user);
    }

    [HttpPatch(Name = nameof(UpdateUser))]
    public async Task<IActionResult> UpdateUser(User user)
    {
        DbContext.Users.Update(user);
        await DbContext.SaveChangesAsync(CancellationToken);

        return Ok(user);
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
