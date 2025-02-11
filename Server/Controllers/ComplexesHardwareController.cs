using DB;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComplexesHardwareController(ApplicationDBContext dbContext) : MainController(dbContext)
{
}
