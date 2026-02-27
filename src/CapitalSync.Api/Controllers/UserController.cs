using CapitalSync.Application.DTOs.Errors;
using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Application.UseCases.Users.ChangePassword;
using CapitalSync.Application.UseCases.Users.Delete;
using CapitalSync.Application.UseCases.Users.GetProfile;
using CapitalSync.Application.UseCases.Users.Register;
using CapitalSync.Application.UseCases.Users.Update;
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

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [EndpointSummary("Delete User Account")]
    [EndpointDescription("Deactivates the authenticated user's account (soft delete).")]
    public async Task<IActionResult> DeleteUserProfile([FromServices] IDeleteUserUseCase useCase)
    {
        await useCase.Execute();

        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [EndpointSummary("Update User Profile")]
    [EndpointDescription("Updates the authenticated user's profile information.")]
    public async Task<IActionResult> UpdateProfile(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPut("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [EndpointSummary("Change User Password")]
    [EndpointDescription("Updates the authenticated user's password after validating the current password.")]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}