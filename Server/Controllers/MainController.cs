using DB;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

public abstract class MainController(ApplicationDBContext dbContext) : Controller
{
    protected ApplicationDBContext DbContext = dbContext;

    protected CancellationToken CancellationToken = new();

}
