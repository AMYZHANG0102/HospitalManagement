/*Hira
Summary: PatientController represents the API controller for managing patient users.
*/
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using Microsoft.AspNetCore.Identity;
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
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
    {
        var patients = await _repository.GetAllAsync();
        return Ok(patients);
    }

    // GET: api/patients/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(string id)
    {
        var patient = await _repository.GetByIdAsync(id);
        if (patient == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        return Ok(patient);
    }

    // //Get: api/patients/appointments/{id}
    // [HttpGet("appointments/{id}")]
    // public async Task<ActionResult<IEnumerable<Appointment>>> GetPatientAppointments(int id)
    // {
    //     var appointments = await _repository.GetPatientAppointmentsAsync(id);
    //     if (appointments == null || !appointments.Any())
    //     {
    //         return Ok(appointments);
    //     }
    //     return Ok(appointments);
    // }


    // // Get: api/patients/reviews/{id}
    // [HttpGet("reviews/{id}")]
    // public async Task<ActionResult<IEnumerable<Review>>> GetPatientReviews(int id)
    // {
    //     var reviews = await _repository.GetPatientReviewsAsync(id);
    //     if (reviews == null || !reviews.Any())
    //     {
    //         return Ok(reviews);
    //     }
    //     return Ok(reviews);
    // }

    // POST: api/patients
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
    public async Task<IActionResult> PatchPatient(string id, [FromBody]
    JsonPatchDocument<UserPatchDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var patient = await _repository.GetByIdAsync(id);
        if (patient == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        // patchDoc.ApplyTo(patient, ModelState);
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        await _repository.UpdateAsync(patient);
        return Ok(patient);
    }

    // DELETE: api/patients/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(string id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        return NoContent();
    }
}