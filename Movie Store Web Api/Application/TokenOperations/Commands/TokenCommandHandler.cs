using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Encryption;
using Movie_Store_Web_Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movie_Store_Web_Api.Application.TokenOperations.Commands;

public class TokenCommandHandler
{
    private readonly IMovieStoreDbContext dbContext;
    private readonly JwtConfig jwtConfig;
    public TokenRequestModel RequestModel { get; set; }

    public TokenCommandHandler(IMovieStoreDbContext dbContext, JwtConfig jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig;
    }

    public TokenResponseModel Handle()
    {
        var entity = dbContext.Customers.SingleOrDefault(x => x.Id == RequestModel.CustomerId);

        if(entity == null)
        {
            throw new InvalidOperationException("Invalid user informations!");
        }

        var md5 = Md5.Create(RequestModel.Password.ToLower());

        if(entity.Password != md5)
        {
            throw new InvalidOperationException("Invalid user informations!");
        }

        string token = Token(entity);

        TokenResponseModel tokenResponse = new TokenResponseModel
        {
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            Email = entity.Email,
            Name = entity.FirstName.ToLower() + " " + entity.LastName.ToLower()
        };

        return tokenResponse;
    }

    private Claim[] GetClaims(Customer customer)
    {
        var claims = new[]
        {
            new Claim("Id", customer.Id.ToString()),
            new Claim("Email", customer.Email),
            new Claim("FullName", $"{customer.FirstName.ToLower()} {customer.LastName.ToLower()}"),
        };
        return claims;
    }

    private string Token(Customer user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials : new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
}


public class TokenRequestModel
{
    public int CustomerId { get; set; }
    public string Password { get; set; }
}

public class TokenResponseModel
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}