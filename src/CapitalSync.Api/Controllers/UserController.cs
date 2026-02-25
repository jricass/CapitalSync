using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.UseCases.Users.Register;
using Microsoft.AspNetCore.Mvc;

namespace CapitalSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}