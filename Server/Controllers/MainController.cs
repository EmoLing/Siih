using AutoMapper;
using DB;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

public abstract class MainController(IMapper mapper) : ControllerBase
{
}
