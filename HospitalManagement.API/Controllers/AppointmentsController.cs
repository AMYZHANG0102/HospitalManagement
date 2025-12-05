/* Amy Zhang
Summary: AppointmentsController to manage appointment-related API endpoints */

using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
namespace HospitalManagement.API.Controllers;

[Authorize] // All endpoints are accessible through JWT token
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepo;

    public AppointmentsController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepo = appointmentRepository;
    }

    // GET: /api/appointments
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments(
        [FromQuery] AppointmentStatus? status,
        [FromQuery] DateOnly? date,
        [FromQuery] AppointmentType? type
    )
    {
        var appointments = await _appointmentRepo.GetAllAsync(null, null, status, date, type);
        return Ok(appointments);
    }

    // GET: /api/appointments/unavailabledoctor
    [Authorize(Roles = "Admin")]
    [HttpGet("unavailabledoctor")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsWhereDoctorIsUnavailable()
    {
        var appointments = await _appointmentRepo.GetAllWhereDoctorIsUnavailable();
        return Ok(appointments);
    }

    // GET: /api/appointments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await _appointmentRepo.GetByIdAsync(id);
        if (appointment == null)
        {
            return NotFound(new {message = $"Appointment with id {id} not found"});
        }
        return Ok(appointment);
    }

    // POST: /api/appointments
    [Authorize(Roles = "Admin, Patient")]
    [HttpPost]
    public async Task<ActionResult<Appointment>> CreateAppointment(
        [FromBody] AppointmentCreateDto appointmentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newAppointment = new Appointment
        {
            PatientId = appointmentCreateDto.PatientId,
            DoctorId = appointmentCreateDto.DoctorId,
            Type = appointmentCreateDto.Type,
            DateTime = appointmentCreateDto.DateTime
        };

        var createdAppointment = await _appointmentRepo.CreateAsync(newAppointment);
        return CreatedAtAction(
            nameof(GetAppointment),
            new { id = createdAppointment.Id },
            createdAppointment
        );
    }

    // PUT: /api/appointments/{id}
    [Authorize(Roles = "Admin, Patient")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Appointment>> UpdateAppointment(int id,
        [FromBody] AppointmentCreateDto appointmentUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exisits = await _appointmentRepo.ExistsAsync(id);

        if (!exisits)
        {
            return NotFound(new {message = $"Appointment with {id} not found"});
        }

        // Map DTO to appointment entity
        var appointment = new Appointment
        {
            PatientId = appointmentUpdateDto.PatientId,
            DoctorId = appointmentUpdateDto.DoctorId,
            Type = appointmentUpdateDto.Type,
            DateTime = appointmentUpdateDto.DateTime
        }

        var updatedAppointment = await _appointmentRepo.UpdateAsync(appointment);
        return Ok(updatedAppointment);
    }

    // PATCH: /api/appointments/{id}
    [Authorize(Roles = "Admin, Patient")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<Appointment>> PatchAppointment(int id,
        [FromBody] JsonPatchDocument<AppointmentPatchDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new {message = "Patch document is null"});
        }

        var existingAppointment = await _appointmentRepo.GetByIdAsync(id);

        if (existingAppointment == null)
        {
            return NotFound(new {message = $"Appointment with id {id} not found"});
        }

        // Map existing apppintment to DTO
        var appointmentToPatch = new AppointmentPatchDto
        {
            DoctorId = existingAppointment.DoctorId,
            Type = existingAppointment.Type,
            DateTime = existingAppointment.DateTime,
            Status = existingAppointment.Status,
            DoctorIsUnavaliable = existingAppointment.DoctorIsUnavailable
        };

        // Apply patch
        patchDoc.ApplyTo(appointmentToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Map patched DTO back to entity
        existingAppointment.DoctorId = appointmentToPatch.DoctorId;
        existingAppointment.Type = appointmentToPatch.Type;
        existingAppointment.DateTime = appointmentToPatch.DateTime;
        existingAppointment.Status = appointmentToPatch.Status;
        existingAppointment.DoctorIsUnavailable = appointmentToPatch.DoctorIsUnavaliable;

        // Save updates
        var patchedAppointment = await _appointmentRepo.UpdateAsync(existingAppointment);
        return Ok(patchedAppointment);
    }

    // DELETE: /api/appointments/{id}
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var deleted = await _appointmentRepo.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new {message = $"Appointment with {id} not found"});
        }
        return NoContent();
    }

}