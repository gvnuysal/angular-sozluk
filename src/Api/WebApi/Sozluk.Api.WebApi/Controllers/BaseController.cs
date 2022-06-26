using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Sozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController:ControllerBase
{
    public Guid UserId => new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}