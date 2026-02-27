using CapitalSync.Application.DTOs.Errors;
using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Application.UseCases.Login.DoLogin;
using Microsoft.AspNetCore.Mvc;

namespace CapitalSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
     /*
    Por questões de segurança, informações sensíveis devem ser passadas
    no corpo da requisição. Por isso o POST. Outros métodos não aceitam
    injeções no Body.
    */
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginUserJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}