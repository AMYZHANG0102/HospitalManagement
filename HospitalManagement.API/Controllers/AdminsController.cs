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
using Microsoft.AspNetCore.Authorization;
namespace HospitalManagement.API.Controllers;

[Authorize(Roles = "Admin")] // Only admins can access these endpoints with a JWT token
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllAdmins()
    {
        var users = await _userRepo.GetAllAsync();
        var admins = users.Where(u => u.Role == UserRole.Admin);
        return Ok(admins);
    }

    // GET: /api/admins/{id}
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
    [HttpPost]
    public async Task<ActionResult<User>> CreateAdmin([FromBody] UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newAdmin = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Role = UserRole.Admin,
            Phone = userRegisterDto.Phone,
            Email = userRegisterDto.Email,
            Gender = userRegisterDto.Gender,
            Birthdate = userRegisterDto.Birthdate,
            HomeAddress = userRegisterDto.HomeAddress
        };

        var createdAdmin = await _userRepo.CreateAsync(newAdmin);
        return CreatedAtAction(
            nameof(GetAdmin),
            new { id = createdAdmin.Id },
            createdAdmin
        );
    }

    // PUT: /api/admins/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Shift>> UpdateAdmin(string id,
        [FromBody] UserUpdateDto userUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exists = await _userRepo.ExistsAsync(id);

        if (!exists)
        {
            return NotFound(new {message = $"Admin with {id} not found"});
        }

        // Map DTO to shift entity
        var admin = new User
        {
            Id = id,
            FirstName = userUpdateDto.FirstName,
            LastName = userUpdateDto.LastName,
            Phone = userUpdateDto.Phone,
            Gender = userUpdateDto.Gender,
            Birthdate = userUpdateDto.Birthdate,
            HomeAddress = userUpdateDto.HomeAddress
        };

        var updatedShift = await _userRepo.UpdateAsync(admin);
        return Ok(updatedShift);
    }

    // PATCH: /api/admins/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAdmin(string id,
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
            Gender = existingAdmin.Gender,
            Birthdate = existingAdmin.Birthdate,
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
        existingAdmin.Gender = adminToPatch.Gender;
        existingAdmin.Birthdate = adminToPatch.Birthdate;
        existingAdmin.HomeAddress = adminToPatch.HomeAddress;
        
        var patchedAdmin = await _userRepo.UpdateAsync(existingAdmin);
        return Ok(patchedAdmin);
    }

    // DELETE: /api/admins/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdmin(string id)
    {
        var deleted = await _userRepo.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new {message = $"Admin with {id} not found"});
        }
        return NoContent();
    }
}