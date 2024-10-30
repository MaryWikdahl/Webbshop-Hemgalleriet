using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;
using WebApplication1.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserController(UserManager<User> userManager/*, SignInManager<User> signInManager*/)
    {
        _userManager = userManager;
        //_signInManager = signInManager;
    }

    [HttpGet("secure-resource")]
    [Authorize]
    public IActionResult SecureResource()
    {
        // Skyddad resurs - endast åtkomlig för autentiserade användare med giltig JWT-token
        return Ok(new { Message = "This is a secure resource accessible only to authenticated users." });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        if (ModelState.IsValid)
        {
            //var user = new User { Username = model.Username, Email = model.Email };
            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    await _signInManager.SignInAsync(user, isPersistent: false);
            //    return Ok(new { Message = "Registration successful" });
            //}
            //else
            //{
            //    return BadRequest(new { Message = "Registration failed", Errors = result.Errors });
            //}
        }
        return BadRequest(new { Message = "Invalid registration data" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        if (ModelState.IsValid)
        {
            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            //if (result.Succeeded)
            //{
            //    var user = await _userManager.FindByEmailAsync(model.Email);
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var key = Encoding.ASCII.GetBytes("supersecretkey123"); // Samma hemliga nyckel som du använde vid konfigurationen

            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //             {
            //                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            //             }),
            //        Expires = DateTime.UtcNow.AddHours(1),
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };


            //    var token = tokenHandler.CreateToken(tokenDescriptor);
            //    var tokenString = tokenHandler.WriteToken(token);

            //    return Ok(new { Token = tokenString });
            //}
            //else
            //{
            //    return BadRequest(new { Message = "Login failed" });
            //}
        }
        return BadRequest(new { Message = "Invalid login data" });
    }



}
