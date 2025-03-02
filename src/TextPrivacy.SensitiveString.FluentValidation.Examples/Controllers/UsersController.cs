using Microsoft.AspNetCore.Mvc;
using TextPrivacy.SensitiveString.FluentValidation.Examples.Requests;

namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserRequest req)
    {
        return Ok();
    }
}