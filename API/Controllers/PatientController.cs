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
    public ActionResult<IEnumerable<Patient>> SearchPatient(string? dni, bool? insurance)
    {
        var patients = _patientService.GetPatients(dni, insurance);

        if (!patients.Any())
        {
            return NotFound();
        }

        var transformedPatients = patients.Select(
            p => new
            {
                p.PatientId,
                p.Name,
                p.Dni,
                p.Password,
                BirthDate = p.BirthDate.ToString("yyyy-MM-dd"),
                p.Weight,
                p.Height,
                p.Insurance
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
            var patient = patients.FirstOrDefault(p => p.PatientId == PatientId);

            if (patient == null)
            {
                return NotFound("No se encontró el paciente con número de registro " + PatientId);
            }

            var transformedPatient = new
            {
                patient.PatientId,
                patient.Name,
                patient.Dni,
                BirthDate = patient.BirthDate.ToString("yyyy-MM-dd"),
                patient.Weight,
                patient.Height,
                patient.Insurance
            };

            return Ok(transformedPatient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ha ocurrido un error: {ex.Message}");
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
            var patient = patients.FirstOrDefault(p => p.PatientId == patientId);

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
            var patient = patients.FirstOrDefault(p => p.PatientId == patientId);

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