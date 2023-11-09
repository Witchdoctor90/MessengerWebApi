using System.Security.Claims;
using MessengerWebApi.Models.Db;
using MessengerWebApi.Models.DTO;
using MessengerWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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
        return Ok();
    }
}

