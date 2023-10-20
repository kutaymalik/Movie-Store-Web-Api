namespace Movie_Store_Web_Api.Entities;

public class JwtConfig
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiration { get; set; }
}
