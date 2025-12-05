/*Hira
Summary: PatientController represents the API controller for managing patient users.
*/
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace HospitalManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<PatientsController> _logger;
    private readonly IPatientRepository _repository;
    public PatientsController(
        IPatientRepository repository,
        UserManager<User> userManager,
        ILogger<PatientsController> logger)
    {
        _repository = repository;
        _userManager = userManager;
        _logger = logger;
    }

    // GET: api/patients
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
    {
        var patients = await _repository.GetAllPatientsAsync();
        return Ok(patients);
    }

    // GET: api/patients/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Patient>> GetPatientById(string id)
    {
        var patient = await _repository.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        return Ok(patient);
    }

    //Get: api/patients/appointments/{id}
    [HttpGet("appointments/{id}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAllPatientAppointments(string id)
    {
        var appointments = await _repository.GetAllPatientAppointmentsAsync(id);
        if (appointments == null || !appointments.Any())
        {
            return Ok(appointments);
        }
        return Ok(appointments);
    }


    // Get: api/patients/reviews/{id}
    [HttpGet("reviews/{id}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Review>>> GetAllPatientReviews(string id)
    {
        var reviews = await _repository.GetAllPatientReviewsAsync(id);
        if (reviews == null || !reviews.Any())
        {
            return Ok(reviews);
        }
        return Ok(reviews);
    }

    // POST: api/patients
    // No Authorize here because this is used for registration
    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient([FromBody] PatientCreateDto patientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var patient = new Patient
        {
            UserName = patientDto.UserName,
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Gender = patientDto.Gender,
            HealthCard = patientDto.HealthCard,
            HomeAddress = patientDto.HomeAddress,
            Phone = patientDto.Phone,
            Email = patientDto.Email,
        };

        var result = await _userManager.CreateAsync(patient, patientDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = $"Registration failed: {errors}"
            });
        }

        _logger.LogInformation("User {Email} registered successfully", patient.Email);

        return Ok(new AuthResponseDto
        {
            Success = true,
            Message = "User registered successfully",
            UserId = patient.Id,
            Email = patient.Email,
            UserName = patient.UserName
        });
    }

    // PUT: api/patients/{id}
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdatePatient(string id, [FromBody] PatientUpdateDto patientDto)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     var exists = await _repository.ExistsAsync(id);
    //     if (!exists)
    //     {
    //         return NotFound(new { message = $"Patient with ID {id} not found" });
    //     }
    //     var patient = new Patient
    //     {
    //         Id = id,
    //         FirstName = patientDto.FirstName,
    //         LastName = patientDto.LastName,
    //         Gender = patientDto.Gender,
    //         HealthCard = patientDto.HealthCard,
    //         HomeAddress = patientDto.HomeAddress, 
    //         Phone = patientDto.Phone,
    //         Email = patientDto.Email,
    //     };
    //     var updatedPatient = await _repository.UpdateAsync(patient);
    //     return Ok(updatedPatient);
    // }

    // PATCH: api/patients/{id}
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> PatchPatient(string id, [FromBody]
    JsonPatchDocument<UserPatchDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var patient = await _repository.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        // patchDoc.ApplyTo(patient, ModelState);
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        await _repository.UpdatePatientAsync(patient);
        return Ok(patient);
    }

    // Deactivate: api/patients/deactivate/{id}
    [HttpPost("deactivate/{id}")]
    [Authorize]
    public async Task<IActionResult> DeactivatePatient(string id)
    {
        var patient = await _repository.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }

        patient.IsDeactivated = true;
        await _repository.UpdatePatientAsync(patient);

        _logger.LogInformation("Patient {Id} deactivated their account", patient.Id);

        return Ok(new { message = "Account deactivated successfully" });
    }
}