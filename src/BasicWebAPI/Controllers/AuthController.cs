using BasicWebApi.Shared.Models.Req.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthentication.JwtBearer;
using BasicWebApi.Shared.Models.Res.Auth;
using OperationResults.AspNetCore;
using OperationResults;
using BasicWebApi.BusinessLayer.Services.Interface;

namespace BasicWebAPI.Controllers;
public class AuthController : ControllerBase
{
   private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    public IAuthService AuthService { get; }
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await this.authService.Login(loginRequest);
        return HttpContext.CreateResponse(result);
    }
}
