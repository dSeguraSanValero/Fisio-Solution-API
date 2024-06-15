using Microsoft.AspNetCore.Mvc;
using FisioSolution.Business;
using FisioSolution.Models;

namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{

    private readonly ILogger<PatientController> _logger;

    private readonly IPatientService _patientService;

    public PatientController(ILogger<PatientController> logger, IPatientService patientService)
    {
        _logger = logger;
        _patientService = patientService;
    }



    [HttpGet(Name = "GetAllPatients")]
    public ActionResult<Dictionary<int, object>> SearchPatient(string? dni, bool? insurance)
    {
        var patients = _patientService.GetPatients(dni, insurance);

        if (patients.Count == 0)
        {
            return NotFound();
        }

        var transformedPatients = patients.ToDictionary(
            kvp => kvp.Key,
            kvp => new
            {
                kvp.Value.PatientId,
                kvp.Value.Name,
                kvp.Value.Dni,
                kvp.Value.Password,
                BirthDate = kvp.Value.BirthDate.ToString("yyyy-MM-dd"),
                kvp.Value.Weight,
                kvp.Value.Height,
                kvp.Value.Insurance
            }
        );

        return Ok(transformedPatients);
    }



    [HttpGet("{PatientId}", Name = "GetPatient")]
    public IActionResult GetPatient(int PatientId)
    {
        try
        {
            var patients = _patientService.GetPatients(null, null);
            var patient = patients.FirstOrDefault(p => p.Value.PatientId == PatientId).Value;

            if (patient == null)
            {
                return NotFound();
            }

            var transformedPatient = new
            {
                patient.PatientId,
                patient.Name,
                patient.Dni,
                patient.Password,
                BirthDate = patient.BirthDate.ToString("yyyy-MM-dd"),
                patient.Weight,
                patient.Height,
                patient.Insurance
            };           

            return Ok(patient);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No se encontró el paciente con número de registro " + PatientId);
        }
    }


    [HttpPost]
    public IActionResult RegisterPatient([FromBody] RegisterPatientDTO patientDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _patientService.RegisterPatient(
                name: patientDTO.Name,
                dni: patientDTO.Dni,
                password: patientDTO.Password,
                birthDate: patientDTO.BirthDate,
                weight: patientDTO.Weight,
                height: patientDTO.Height,
                insurance: patientDTO.Insurance
            );

            return CreatedAtAction(nameof(SearchPatient), new { dni = patientDTO.Dni }, patientDTO);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{patientId}")]
    public IActionResult DeletePatientById(int patientId)
    {
        try
        {
            var patients = _patientService.GetPatients(null, null);
            var patient = patients.FirstOrDefault(p => p.Value.PatientId == patientId).Value;

            if (patient == null)
            {
                return NotFound();
            }

            _patientService.DeletePatient(patient);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
    }


    [HttpPut("{patientId}")]
    public IActionResult UpdatePatient(int patientId, [FromBody] UpdatePatientDTO patientDTO)
    {
        try
        {
            var patients = _patientService.GetPatients(null, null);
            var patient = patients.FirstOrDefault(p => p.Value.PatientId == patientId).Value;

            if (patient == null)
            {
                return NotFound();
            }

            _patientService.UpdatePatient(patient, 
                password: patientDTO.Password,
                weight: patientDTO.Weight,
                height: patientDTO.Height,
                insurance: patientDTO.Insurance
            );

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el paciente.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al procesar la solicitud.");
        }
    }
}