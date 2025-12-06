/* Amy Zhang
Summary: ShiftsController to manage shift-related API endpoints */

using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
namespace HospitalManagement.API.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly IShiftRepository _shiftRepo;

    public ShiftsController(IShiftRepository shiftRepository)
    {
        _shiftRepo = shiftRepository;
    }

    // GET: /api/shifts
    //[Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftReadDto>>> GetAllShifts()
    {
        var shifts = await _shiftRepo.GetAllAsync();
        List<ShiftReadDto> shiftsToReturn = new();
        foreach (var s in shifts)
        {
            var shiftReadDto = new ShiftReadDto
            {
                Id = s.Id,
                DoctorId = s.DoctorId,
                DoctorName = s.Doctor?.FirstName,
                StartDateTime = s.StartDateTime,
                EndDateTime = s.EndDateTime
            };
            shiftsToReturn.Add(shiftReadDto);
        }
        return Ok(shiftsToReturn);
    }

    // GET: /api/shifts/{id}
    //[Authorize(Roles = "Admin, Doctor")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftReadDto>> GetShiftById(int id)
    {
        var shift = await _shiftRepo.GetByIdAsync(id);
        if (shift == null)
        {
            return NotFound();
        }
        var shiftReadDto = new ShiftReadDto
        {
            Id = shift.Id,
            DoctorId = shift.DoctorId,
            DoctorName = shift.Doctor?.FirstName,
            StartDateTime = shift.StartDateTime,
            EndDateTime = shift.EndDateTime
        };
        return Ok(shiftReadDto);
    }

    // POST: /api/shifts
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Shift>> CreateShift([FromBody] ShiftCreateDto shiftCreateDto)
    {
        var shift = new Shift
        {
            DoctorId = shiftCreateDto.DoctorId,
            StartDateTime = shiftCreateDto.StartDateTime,
            EndDateTime = shiftCreateDto.EndDateTime
        };

        var createdShift = await _shiftRepo.CreateAsync(shift);
        return CreatedAtAction(
            nameof(GetShiftById),
            new { id = createdShift.Id },
            createdShift);
    }

    // PUT: /api/shifts/{id}
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Shift>> UpdateShift(int id,
        [FromBody] ShiftUpdateDto shiftUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exists = await _shiftRepo.ExistsAsync(id);

        if (!exists)
        {
            return NotFound(new {message = $"Shift with {id} not found"});
        }

        // Map DTO to shift entity
        var shift = new Shift
        {
            Id = id,
            DoctorId = shiftUpdateDto.DoctorId,
            StartDateTime = shiftUpdateDto.StartDateTime,
            EndDateTime = shiftUpdateDto.EndDateTime
        };

        var updatedShift = await _shiftRepo.UpdateAsync(shift);
        return Ok(updatedShift);
    }

    // PATCH: /api/shifts/{id}
    //[Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<Shift>> PatchShift(int id,
        [FromBody] JsonPatchDocument<ShiftPatchDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null"});
        }

        var existingShift = await _shiftRepo.GetByIdAsync(id);
        
        if (existingShift == null)
        {
            return NotFound(new {message = $"Shift with id {id} not found"});
        }

        // Map DTO to existing shift
        ShiftPatchDto shiftToPatch = new ShiftPatchDto
        {
            DoctorId = existingShift.DoctorId,
            StartDateTime = existingShift.StartDateTime,
            EndDateTime = existingShift.EndDateTime
        };

        // Apply the patch to the DTO
        patchDoc.ApplyTo(shiftToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Update the existing shift with patched values
        existingShift.DoctorId = shiftToPatch.DoctorId;
        existingShift.StartDateTime = shiftToPatch.StartDateTime;
        existingShift.EndDateTime = shiftToPatch.EndDateTime;

        var updatedShift = await _shiftRepo.UpdateAsync(existingShift);
        return Ok(updatedShift);
    }

    // DELETE: /api/shifts/{id}
    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var deleted = await _shiftRepo.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new {message = $"Shift with {id} not found"});
        }
        return NoContent();
    }
}