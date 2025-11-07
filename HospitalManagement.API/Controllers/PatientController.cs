using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
namespace HospitalManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
private readonly IPatientRepository _repository;
public PatientsController(IPatientRepository repository)
{
_repository = repository;
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
public async Task<ActionResult<Patient>> GetPatient(int id)
{
var patient = await _repository.GetByIdAsync(id);
if (patient == null)
{
return NotFound(new { message = $"Patient with ID {id} not found" });
}
return Ok(patient);
}



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
FirstName = patientDto.FirstName,
LastName = patientDto.LastName,
Gender = patientDto.Gender,
HealthCard = patientDto.HealthCard,
HomeAddress = patientDto.HomeAddress,
Phone = patientDto.Phone,
Email = patientDto.Email,
};

var createdPatient = await _repository.CreateAsync(patient);
return CreatedAtAction(
nameof(GetPatient),
new { id = createdPatient.Id },
createdPatient
);
}


// PUT: api/patients/{id}
[HttpPut("{id}")]
public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientUpdateDto patientDto)
{
if (!ModelState.IsValid)
{
return BadRequest(ModelState);
}
var exists = await _repository.ExistsAsync(id);
if (!exists)
{
return NotFound(new { message = $"Patient with ID {id} not found" });
}
var patient = new Patient
{
Id = id,
FirstName = patientDto.FirstName,
LastName = patientDto.LastName,
Gender = patientDto.Gender,
HealthCard = patientDto.HealthCard,
HomeAddress = patientDto.HomeAddress, 
Phone = patientDto.Phone,
Email = patientDto.Email,
};
var updatedPatient = await _repository.UpdateAsync(patient);
return Ok(updatedPatient);
}

// PATCH: api/tasks/{id}
[HttpPatch("{id}")]
public async Task<IActionResult> PatchPatient(int id, [FromBody]
JsonPatchDocument<Patient> patchDoc)
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
patchDoc.ApplyTo(patient, ModelState);
if (!ModelState.IsValid)
{
return BadRequest(ModelState);
}

await _repository.UpdateAsync(patient);
return Ok(patient);
}

// DELETE: api/patients/{id}
[HttpDelete("{id}")]
public async Task<IActionResult> DeletePatient(int id)
{
var deleted = await _repository.DeleteAsync(id);
if (!deleted)
{
return NotFound(new { message = $"Patient with ID {id} not found" });
}
return NoContent();
}
}