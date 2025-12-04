/* Amy Zhang
Summary: AppointmentsController to manage appointment-related API endpoints */

using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using HospitalManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
namespace HospitalManagement.API.Controllers;

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
    // Authorize admins
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


    // GET: /api/appointments/{id}
    // Authorize admins, patients, and doctors
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(long id)
    {
        var appointment = await _appointmentRepo.GetByIdAsync(id);
        if (appointment == null)
        {
            return NotFound(new {message = $"Appointment with id {id} not found"});
        }
        return Ok(appointment);
    }

    // POST: /api/appointments
    // Authorize admins and patients
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

    // PATCH: /api/appointments/{id}
    // Authorize admins and patients
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAppointment(long id,
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
            Status = existingAppointment.Status
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

        // Save updates
        var patchedAppointment = await _appointmentRepo.UpdateAsync(existingAppointment);
        return Ok(patchedAppointment);
    }

}