using Microsoft.AspNetCore.Mvc;

namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser()
    {
        return Ok();
    }
}