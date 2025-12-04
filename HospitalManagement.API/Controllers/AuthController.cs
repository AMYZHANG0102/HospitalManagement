/*Hira Ahmad
Summary: AuthController handles user authentication and authorization. It provides endpoints for user registration, login, logout,
password change, retrieving current user info, and account deactivation. It uses JWT for secure token-based authentication.*/
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Models;
namespace IdentityManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] UserRegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Invalid registration data"
            });
        }

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = $"Registration failed: {errors}"
            });
        }

        _logger.LogInformation("User {Email} registered successfully", user.Email);

        return Ok(new AuthResponseDto
        {
            Success = true,
            Message = "User registered successfully",
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName
        });

    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured");
       
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add user roles to claims
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expirationMinutes = int.Parse(jwtSettings["ExpirationInMinutes"] ?? "60");

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDto 
            { 
                Success = false, 
                Message = "Invalid login data" 
                
            });
        }
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Unauthorized(new AuthResponseDto 
            { 
                Success = false, 
                Message = "Invalid email or password" 
            });
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
        
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                return Unauthorized(new AuthResponseDto 
                { 
                    Success = false, 
                    Message = "Account is locked due to multiple failed login attempts." 
                    
                });
            }
            return Unauthorized(new AuthResponseDto 
            { 
                Success = false, 
                Message = "Invalid email or password" 
            });
        }

        // Update last login time 
        user.LastLoginAt = DateTime.UtcNow; 
        await _userManager.UpdateAsync(user); 
        
        // Generate JWT token 
        var token = await GenerateJwtToken(user); 

        _logger.LogInformation("User {Email} logged in successfully", user.Email);
      
        return Ok(new AuthResponseDto
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName
        });

    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult<AuthResponseDto>> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out");
        return Ok(new AuthResponseDto 
        {
            Success = true, 
            Message = "Logout successful" 
        });
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult<AuthResponseDto>> ChangePassword([FromBody] ChangePasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponseDto 
            { 
                Success = false, 
                Message = "Invalid data" 
            });
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized(new AuthResponseDto 
            { 
                Success = false, 
                Message = "User not found" 
            });
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new AuthResponseDto 
            { 
                Success = false, 
                Message = "User not found" 
            });
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new AuthResponseDto 
            { 
                Success = false, 
                Message = $"Password change failed: {errors}" 
            });
        }

        _logger.LogInformation("User {Email} changed password", user.Email);

        return Ok(new AuthResponseDto 
        { 
            Success = true, 
            Message = "Password changed successfully" 
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<AuthResponseDto>> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized(new AuthResponseDto 
            { 
                Success = false, 
                Message = "User not authenticated" 
            });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new AuthResponseDto 
            { 
                Success = false, 
                Message = "User not found" 
            });
        }
        return Ok(new AuthResponseDto
        {
            Success = true,
            Message = "User information retrieved",            
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName
        });

    }

    [Authorize]
    [HttpPost("deactivate-account")]
    public async Task<ActionResult<AuthResponseDto>> DeactivateAccount()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized(new AuthResponseDto 
            { 
                Success = false, 
                Message = "User not authenticated" 
            });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new AuthResponseDto 
            { 
                Success = false, 
                Message = "User not found" 
            });
        }

        user.IsDeactivated = true;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new AuthResponseDto 
            { 
                Success = false, 
                Message = $"Account deactivation failed: {errors}" 
            });
        }

        _logger.LogInformation("User {Email} deactivated their account", user.Email);

       
        await _signInManager.SignOutAsync();  // Optional: sign out the user

        return Ok(new AuthResponseDto 
        { 
            Success = true, 
            Message = "Account deactivated successfully" 
        });
    }

}