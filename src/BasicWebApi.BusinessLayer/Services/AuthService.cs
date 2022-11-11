using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasicWebApi.BusinessLayer.Services.Common;
using BasicWebApi.BusinessLayer.Services.Interface;
using BasicWebApi.DataAccessLayer;
using BasicWebApi.Shared.Models.Req.Auth;
using BasicWebApi.Shared.Models.Res.Auth;
using OperationResults;
using SimpleAuthentication.JwtBearer;

namespace BasicWebApi.BusinessLayer.Services;
public class AuthService : BaseService, IAuthService
{
    private readonly IJwtBearerService jwtBearerService;


    public AuthService(IDataContext context, IMapper mapper, IJwtBearerService jwtBearerService) : base(context, mapper)
    {
        this.jwtBearerService = jwtBearerService;
    }

    public async Task<Result<LoginResponse>> Login(LoginRequest loginRequest)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.GivenName, "Marco"),
            new(ClaimTypes.Surname, "Minerva")
        };

        throw new NotImplementedException();
        var token = jwtBearerService.CreateToken(loginRequest.Username, claims);
        return new LoginResponse() { Token = token };
    }
}
