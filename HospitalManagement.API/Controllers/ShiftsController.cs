/* Amy Zhang
Summary: ShiftsController to manage shift-related API endpoints */

using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
namespace HospitalManagement.API.Controllers;

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
    // Authorize admins
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shift>>> GetAllShifts()
    {
        var shifts = await _shiftRepo.GetAllAsync();
        return Ok(shifts);
    }

    // GET: /api/shifts/{id}
    // Authorize admins and doctors
    [HttpGet("{id}")]
    public async Task<ActionResult<Shift>> GetShiftById(int id)
    {
        var shift = await _shiftRepo.GetByIdAsync(id);
        if (shift == null)
        {
            return NotFound();
        }
        return Ok(shift);
    }

    // POST: /api/shifts
    // Authorize admins
    [HttpPost]
    public async Task<ActionResult<Shift>> CreateShift([FromBody] ShiftCreateDto shiftCreateDto)
    {
        var shift = new Shift
        {
            Doctors = shiftCreateDto.Doctors,
            Date = shiftCreateDto.Date,
            StartTime = shiftCreateDto.StartTime,
            EndTime = shiftCreateDto.EndTime
        };

        var createdShift = await _shiftRepo.CreateAsync(shift);
        return CreatedAtAction(
            nameof(GetShiftById),
            new { id = createdShift.Id },
            createdShift);
    }

    // PATCH: /api/shifts/{id}
    // Authorize admins
    [HttpPatch("{id}")]
    public async Task<ActionResult<Shift>> UpdateShift(int id,
        [FromBody] JsonPatchDocument<Shift> patchDoc)
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
            Doctors = existingShift.Doctors,
            Date = existingShift.Date,
            StartTime = existingShift.StartTime,
            EndTime = existingShift.EndTime
        };

        // Apply the patch to the DTO
        patchDoc.ApplyTo(existingShift, ModelState);

        if (!TryValidateModel(existingShift))
        {
            return ValidationProblem(ModelState);
        }

        // Update the existing shift with patched values
        existingShift.Doctors = shiftToPatch.Doctors;
        existingShift.Date = shiftToPatch.Date;
        existingShift.StartTime = shiftToPatch.StartTime;
        existingShift.EndTime = shiftToPatch.EndTime;

        var updatedShift = await _shiftRepo.UpdateAsync(existingShift);
        return Ok(updatedShift);
    }
}