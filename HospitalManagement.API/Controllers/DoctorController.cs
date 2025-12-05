using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
namespace HospitalManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorRepository _repository;
    public DoctorsController(IDoctorRepository repository)
    {
        _repository = repository;
    }

    // GET: api/doctors
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
    {
        var doctors = await _repository.GetAllAsync();
        return Ok(doctors);
    }

    // GET: api/doctors/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Doctor>> GetDoctor(string id)
    {
        var doctor = await _repository.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found" });
        }
        return Ok(doctor);
    }

    // // GET: api/doctors/schedule/{id}
    // [HttpGet("schedule/{id}")]
    // public async Task<ActionResult<DoctorScheduleDto>> GetDoctorSchedule(int id)
    // {
    //     var schedule = await _repository.GetDoctorScheduleAsync(id);
    //     if (schedule == null)
    //     {
    //         return NotFound(new { message = $"Schedule for Doctor with ID {id} not found" });
    //     }
    //     return Ok(schedule);
    // }

    // // Get: api/doctors/appointments/{id}
    // [HttpGet("appointments/{id}")]
    // public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorAppointments(int id)
    // {
    //     var appointments = await _repository.GetDoctorAppointmentsAsync(id);
    //     if (appointments == null || !appointments.Any())
    //     {
    //         return Ok(appointments);
    //     }
    // }

    // // Get: api/doctors/{id}/reviews
    // [HttpGet("{id}/reviews")]
    // public async Task<ActionResult<IEnumerable<Review>>> GetDoctorReviews(int id)
    // {
    //     var reviews = await _repository.GetDoctorReviewsAsync(id);
    //     if (reviews == null || !reviews.Any())
    //     {
    //         return Ok(reviews);
    //     }
    // }

    // POST: api/doctors
    [HttpPost]
    public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] DoctorCreateDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var doctor = new Doctor
        {
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            Gender = doctorDto.Gender,
            HomeAddress = doctorDto.HomeAddress,
            Phone = doctorDto.Phone,
            Email = doctorDto.Email,
        };

        var createdDoctor = await _repository.CreateAsync(doctor);
        return CreatedAtAction(
            nameof(GetDoctor),
            new { id = createdDoctor.Id },
            createdDoctor
        );
    }

    // PUT: api/doctors/{id}
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateDoctor(string id, [FromBody] DoctorPatchDto doctorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var exists = await _repository.ExistsAsync(id);
        if (!exists)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found" });
        }
        var doctor = new Doctor
        {
            Id = id,
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            HomeAddress = doctorDto.HomeAddress, 
            Phone = doctorDto.Phone,
        };
        var updatedDoctor = await _repository.UpdateAsync(doctor);
        return Ok(updatedDoctor);
    }

    // PATCH: api/doctor/{id}
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> PatchDoctor(string id, [FromBody] JsonPatchDocument<DoctorPatchDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var doctor = await _repository.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found" });
        }
        // patchDoc.ApplyTo(doctor, ModelState);
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        await _repository.UpdateAsync(doctor);
        return Ok(doctor);
    }

    // DELETE: api/doctors/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDoctor(string id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found" });
        }
        return NoContent();
    }
}
