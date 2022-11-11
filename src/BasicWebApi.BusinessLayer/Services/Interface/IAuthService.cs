using BasicWebApi.Shared.Models.Req.Auth;
using BasicWebApi.Shared.Models.Res.Auth;
using OperationResults;

namespace BasicWebApi.BusinessLayer.Services.Interface;
public interface IAuthService
{
    Task<Result<LoginResponse>> Login(LoginRequest loginRequest);
}