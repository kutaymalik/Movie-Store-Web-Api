using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Movie_Store_Web_Api.Application.TokenOperations.Commands;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Entities;

namespace Movie_Store_Web_Api.Controllers;

[ApiController]
[Route("moviestore/api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenController(IMovieStoreDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }

    [HttpPost]
    public IActionResult GetToken([FromBody] TokenRequestModel model)
    {
        TokenCommandHandler command = new(dbContext, jwtConfig);

        command.RequestModel = model;

        var token = command.Handle();
        return Ok(token);
    }
}
