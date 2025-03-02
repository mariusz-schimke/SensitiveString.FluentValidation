using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TextPrivacy.SensitiveString.FluentValidation.Examples.Requests;

namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateUser([FromBody] CreateUserRequest req, IValidator<CreateUserRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(req);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return Results.Ok();
    }
}