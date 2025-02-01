using DB;
using DB.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;
public class UserController : Controller
{
    private ApplicationDBContext _dbContext;
    private CancellationToken _cancellationToken;

    public UserController(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet(Name = nameof(GetUser))]
    public async Task<User> GetUser(int id) => await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, _cancellationToken);

    [HttpGet(Name = nameof(GetUsers))]
    public IEnumerable<User> GetUsers() => [.. _dbContext.Users];

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
