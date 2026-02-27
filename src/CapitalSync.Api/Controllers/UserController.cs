using CapitalSync.Application.DTOs.Errors;
using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Application.UseCases.Users.GetProfile;
using CapitalSync.Application.UseCases.Users.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapitalSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [EndpointSummary("User Register")]
    [EndpointDescription("Executes the registration use case, and returns created response with the registered user details.")]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [EndpointSummary("Get User Profile")]
    [EndpointDescription("Retrieves the authenticated user's profile information.")]
    public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }
}