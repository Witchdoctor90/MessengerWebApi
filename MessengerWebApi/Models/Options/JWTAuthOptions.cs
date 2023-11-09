using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MessengerWebApi.Models.Options;

public class JWTAuthOptions
{
    public const string ISSUER = "MessangerServer";
    public const string AUDIENCE = "MessangerClient";
    private const string KEY = "messangersecret@@992";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}