using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MessengerWebApi.Models.Db;
using MessengerWebApi.Models.DTO;
using MessengerWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MessengerWebApi.Models.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MessengerWebApi.Controllers;

public class AuthController : Controller
{
    private MessengerDbContext _context;

    public AuthController(MessengerDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterDTO user)
    {
        await _context.Users.AddAsync(new User
        {
            FirstName = user.Username,
            Password = user.Password,
            CreatedAt = DateTime.Now,
            IsActive = true,
            IsBlocked = false,
            IsReported = false
        });
        await _context.SaveChangesAsync();
        return Ok();
    }

    public async Task<IActionResult> Login([FromBody] RegisterDTO user)
    {
        Models.Entities.User? identity = await _context.Users.FirstOrDefaultAsync(
            x => x.FirstName == user.Username &&
                 x.Password == user.Password
        );
        
        return null;
    }

    public async Task<IActionResult> Token([FromBody] RegisterDTO user)
    {
        var identity = await GetIdentity(user);
        if (identity == null) return BadRequest("Invalid username or password!");

        var jwt = new JwtSecurityToken(
            issuer: JWTAuthOptions.ISSUER,
            audience: JWTAuthOptions.AUDIENCE,
            claims: identity.Claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.Add(TimeSpan.FromDays(14)),
            signingCredentials: new SigningCredentials(JWTAuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };

        return Json(response);
    }

    public async Task<ClaimsIdentity?> GetIdentity([FromBody] RegisterDTO user)
    {
        Models.Entities.User? identity = await _context.Users.FirstOrDefaultAsync(
            x => x.FirstName == user.Username &&
                 x.Password == user.Password
        );
        
        if (identity == null) return null;
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, identity.FirstName),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        
        return claimsIdentity;
    }
}

