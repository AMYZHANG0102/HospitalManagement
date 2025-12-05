using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Core.DTOs;
using HospitalManagement.Core.Interfaces;
using HospitalManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
namespace HospitalManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class PatientRecordsController : ControllerBase
{
    private readonly IPatientRecordRepository _repository;

    public PatientRecordsController(IPatientRecordRepository repository)
    {
        _repository = repository;
    }

    // GET: api/patientRecords
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientRecord>>> GetAllPatientRecords()
    {
        var patients = await _repository.GetAllAsync();
        return Ok(patients);
    }

    // GET: api/patientRecords/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientRecord>> GetPatientRecord(int id)
    {
        var patient = await _repository.GetByIdAsync(id);
        if (patient == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        return Ok(patient);
    }

    // POST: api/patientRecords
    [HttpPost]
    public async Task<ActionResult<PatientRecord>> CreatePatientRecord([FromBody] PatientRecordCreateDto patientRecordDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var patientRecord = new PatientRecord
        {
            PatientId = patientRecordDto.PatientId,
            Diagnosis = patientRecordDto.Diagnosis,
            CurrentMedications = patientRecordDto.CurrentMedications,
            MedicationAllergyInfo = patientRecordDto.MedicationAllergies,
            BloodType = patientRecordDto.BloodType,
            PastMedicalHistory = patientRecordDto.PastMedicalHistory,
            LabTestsResults = patientRecordDto.LabTestsResults,
            // Notes = patientRecordDto.Notes
        };

        var createdPatientRecord = await _repository.CreateAsync(patientRecord);
        return CreatedAtAction(
            nameof(GetPatientRecord),
            new { id = createdPatientRecord.Id },
            createdPatientRecord
        );
    }

    // PUT: api/patientRecords/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatientRecord(int id, [FromBody] PatientRecordUpdateDto patientRecordDto)
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
        var patientRecord = new PatientRecord
        {
            PatientId = patientRecordDto.PatientId,
        };
        var updatedPatientRecord = await _repository.UpdateAsync(patientRecord);
        return Ok(updatedPatientRecord);
    }

    // PATCH: api/patientRecords/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPatientRecord(int id, [FromBody]
    JsonPatchDocument<PatientRecord> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }
        var patientRecord = await _repository.GetByIdAsync(id);
        if (patientRecord == null)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        patchDoc.ApplyTo(patientRecord, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _repository.UpdateAsync(patientRecord);
        return Ok(patientRecord);
    }

    // DELETE: api/patientRecords/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatientRecord(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Patient with ID {id} not found" });
        }
        return NoContent();
    }
}