using AutoMapper;
using Core.Models.Users;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Users;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserService service, IMapper mapper) : MainController(mapper)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserObject>> GetUser(int id)
    {
        var user = await service.GetUserAsync(id);

        if (user is null)
            BadRequest(id);

        var foundUser = mapper.Map<UserObject>(user);

        return Ok(foundUser);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserObject>>> GetUsers()
    {
        var coreUsers = await service.GetUsersAsync();
        var serverUsers = mapper.Map<IEnumerable<UserObject>>(coreUsers);
        return Ok(serverUsers);
    }

    [HttpPost]
    public async Task<ActionResult<UserObject>> AddUser([FromBody] UserObject user)
    {
        if (user is null)
            return BadRequest("Данные пользователя не предоставлены.");

        var coreUser = mapper.Map<User>(user);
        var createdCoreUser = await service.AddUserAsync(coreUser);
        var createdUser = mapper.Map<UserObject>(coreUser);
        
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserObject user, CancellationToken cancellationToken)
    {
        if (user is null)
            return BadRequest();

        var coreUser = mapper.Map<User>(user);
        var createdCoreUser = await service.UpdateUserAsync(coreUser, cancellationToken);

        return createdCoreUser is not null ? Ok() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        bool result = await service.DeleteUserAsync(id);
        return result ? Ok() : BadRequest();
    }
}
