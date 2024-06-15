using Microsoft.AspNetCore.Mvc;
using FisioSolution.Models;
using FisioSolution.Business;


namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TreatmentController : ControllerBase
{

    private readonly ILogger<TreatmentController> _logger;

    private readonly ITreatmentService _treatmentService;

    public TreatmentController(ILogger<TreatmentController> logger, ITreatmentService treatmentService)
    {
        _logger = logger;
        _treatmentService = treatmentService;
    }


    [HttpGet(Name = "GetAllTreatments")]
    public ActionResult<IEnumerable<object>> SearchTreatment(int? physioId, int? patientId)
    {
        var treatments = _treatmentService.GetTreatments(physioId, patientId);

        if (!treatments.Any())
        {
            return NotFound();
        }

        var transformedTreatments = treatments.Select(p => new
        {
            p.TreatmentId,
            p.PatientId,
            p.PhysioId,
            p.TreatmentCause,
            TreatmentDate = p.TreatmentDate.ToString("yyyy-MM-dd"),
            p.MoreSessionsNeeded,
        });

        return Ok(transformedTreatments);
    }



    [HttpGet("{TreatmentId}", Name = "GetTreatment")]
    public IActionResult GetTreatment(int TreatmentId)
    {
        try
        {
            var treatments = _treatmentService.GetTreatments(null, null);
            var treatment = treatments.FirstOrDefault(p => p.TreatmentId == TreatmentId);

            if (treatment == null)
            {
                return NotFound();
            }

            var transformedTreatment = new
            {
                treatment.TreatmentId,
                treatment.PatientId,
                treatment.PhysioId,
                treatment.TreatmentCause,
                TreatmentDate = treatment.TreatmentDate.ToString("yyyy-MM-dd"),
                treatment.MoreSessionsNeeded
            };    

            return Ok(treatment);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No se encontró el tratamiento con id " + TreatmentId);
        }
    }


    [HttpPost]
    public IActionResult RegisterTreatment([FromBody] RegisterTreatmentDTO treatmentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _treatmentService.RegisterTreatment(
                physioId: treatmentDTO.PhysioId,
                patientId: treatmentDTO.PatientId,
                treatmentCause: treatmentDTO.TreatmentCause,
                treatmentDate: treatmentDTO.TreatmentDate,
                moreSessionsNeeded: treatmentDTO.MoreSessionsNeeded
            );

            return Ok("Tratamiento registrado correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{treatmentId}")]
    public IActionResult DeleteTreatmentById(int treatmentId)
    {
        try
        {
            var treatments = _treatmentService.GetTreatments(null, null);
            var treatment = treatments.FirstOrDefault(p => p.TreatmentId == treatmentId);


            if (treatment == null)
            {
                return NotFound();
            }

            _treatmentService.DeleteTreatment(treatment);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el tratamiento.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al procesar la solicitud.");
        }
    }
}