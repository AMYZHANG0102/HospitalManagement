/* Amy Zhang
Summary: AdminController represents the API controller for managing admin users.
It does NOT represent the admin role or the actions that an admin user can perform.
We are using the general IUserRepository because the admins do not have any
special properties that differentiate them from regular users.
Admins are simply users with elevated permissions, so we can use the same user model and repository. */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
namespace HospitalManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly IUserRepository _userRepo;

    public AdminsController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    // GET: /api/admins
    // Authorize: Admins
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllAdmins()
    {
        var users = await _userRepo.GetAllAsync();
        var admins = users.Where(u => u.Role == UserRole.Admin);
        return Ok(admins);
    }

    // GET: /api/admins/{id}
    // Authorize: Admins
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetAdmin(string id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null || user.Role != UserRole.Admin)
        {
            return NotFound(new {message = $"Admin with ID {id} not found"});
        }
        return Ok(user);
    }

    // POST: /api/admins
    // Authorize: Admins
    [HttpPost]
    public async Task<ActionResult<User>> CreateAdmin([FromBody] UserRegisterDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newAdmin = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Role = UserRole.Admin,
            Phone = userDto.Phone,
            Email = userDto.Email,
            Gender = userDto.Gender,
            Birthdate = userDto.Birthdate,
            HomeAddress = userDto.HomeAddress
        };

        var createdAdmin = await _userRepo.CreateAsync(newAdmin);
        return CreatedAtAction(
            nameof(GetAdmin),
            new { id = createdAdmin.Id },
            createdAdmin
        );
    }

    // PATCH: /api/admins/{id}
    // Authorize: Admins
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateAdmin(string id,
        [FromBody] JsonPatchDocument<UserPatchDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new {message = "Patch document is null"});
        }

        var existingAdmin = await _userRepo.GetByIdAsync(id);

        if (existingAdmin == null || existingAdmin.Role != UserRole.Admin)
        {
            return NotFound(new {message = $"Admin with ID {id} not found"});
        }

        // Map User entitity to UserPatchDto
        var adminToPatch = new UserPatchDto
        {
            FirstName = existingAdmin.FirstName,
            LastName = existingAdmin.LastName,
            Phone = existingAdmin.Phone,
            HomeAddress = existingAdmin.HomeAddress
        };

        // Apply the patch
        patchDoc.ApplyTo(adminToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Map back the patched fields to the existing admin entity
        existingAdmin.FirstName = adminToPatch.FirstName;
        existingAdmin.LastName = adminToPatch.LastName;
        existingAdmin.Phone = adminToPatch.Phone;
        existingAdmin.HomeAddress = adminToPatch.HomeAddress;
        
        var patchedAdmin = await _userRepo.UpdateAsync(existingAdmin);
        return Ok(patchedAdmin);
    }
}