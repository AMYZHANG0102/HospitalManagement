using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
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
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
    {
        var doctors = await _repository.GetAllAsync();
        return Ok(doctors);
    }

    // GET: api/doctors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(string id)
    {
        var doctor = await _repository.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found" });
        }
        return Ok(doctor);
    }

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
            Phone = doctorDto.Phone,
            Gender = doctorDto.Gender,
            Birthdate = doctorDto.Birthdate,
            HomeAddress = doctorDto.HomeAddress,
            Specialization = doctorDto.Specialization
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
    public async Task<ActionResult<Doctor>> UpdateDoctor(string id,
        [FromBody] DoctorUpdateDto doctorDto)
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
            Phone = doctorDto.Phone,
            Gender = doctorDto.Gender,
            Birthdate = doctorDto.Birthdate,
            HomeAddress = doctorDto.HomeAddress,
            Specialization = doctorDto.Specialization
        };
        var updatedDoctor = await _repository.UpdateAsync(doctor);
        return Ok(updatedDoctor);
    }

    // PATCH: /api/doctors/{id}
    [HttpPatch("{id}")]
    public async Task<ActionResult<Doctor>> PatchDoctor(string id,
        [FromBody] JsonPatchDocument<Doctor> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null"});
        }

        var existingDoctor = await _repository.GetByIdAsync(id);
        
        if (existingDoctor == null)
        {
            return NotFound(new {message = $"Doctor with id {id} not found"});
        }

        // Map DTO to existing doctor
        DoctorPatchDto doctorToPatch = new DoctorPatchDto
        {
            FirstName = existingDoctor.FirstName,
            LastName = existingDoctor.LastName,
            Phone = existingDoctor.Phone,
            Gender = existingDoctor.Gender,
            Birthdate = existingDoctor.Birthdate,
            HomeAddress = existingDoctor.HomeAddress,
            Specialization = existingDoctor.Specialization
        };

        // Apply the patch to the DTO
        patchDoc.ApplyTo(existingDoctor, ModelState);

        // Apply the patch to the DTO
        patchDoc.ApplyTo(existingDoctor, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Update the existing doctor with patched values
        existingDoctor.FirstName = doctorToPatch.FirstName;
        existingDoctor.LastName = doctorToPatch.LastName;
        existingDoctor.Phone = doctorToPatch.Phone;
        existingDoctor.Gender = doctorToPatch.Gender;
        existingDoctor.Birthdate = doctorToPatch.Birthdate;
        existingDoctor.Specialization = doctorToPatch.Specialization;

        var patchedDoctor = await _repository.UpdateAsync(existingDoctor);
        return Ok(patchedDoctor);
    }
    
    // DELETE: api/doctors/{id}
    [HttpDelete("{id}")]
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
