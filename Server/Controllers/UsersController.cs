using DB;
using DB.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private ApplicationDBContext _dbContext;
    private CancellationToken _cancellationToken;

    public UsersController(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{id}")]
    public async Task<User> GetUser(int id) => await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, _cancellationToken);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    [HttpPost(Name = nameof(CreateUser))]
    public async Task<IActionResult> CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user, _cancellationToken);
        await _dbContext.SaveChangesAsync(_cancellationToken);

        return Ok(user);
    }

    [HttpPatch(Name = nameof(UpdateUser))]
    public async Task<IActionResult> UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(_cancellationToken);

        return Ok(user);
    }

    [HttpDelete(Name = nameof(DeleteUser))]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = _dbContext.Users.Find(id);

        if (user is null)
            return NotFound(id);

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return Ok(user);
    }
}
