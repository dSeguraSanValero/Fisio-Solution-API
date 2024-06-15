using Microsoft.AspNetCore.Mvc;
using FisioSolution.Data;
using FisioSolution.Models;
using FisioSolution.Business;

namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhysioController : ControllerBase
{
    private readonly FisioSolutionContext _context;

    private readonly ILogger<PhysioController> _logger;

    private readonly IPhysioService _physioService;

    public PhysioController(FisioSolutionContext context, ILogger<PhysioController> logger, IPhysioService physioService)
    {
        _logger = logger;
        _context = context;
        _physioService = physioService;
    }

    [HttpGet(Name = "GetAllPhysios")]
    public ActionResult<Dictionary<int, Physio>> SearchPhysio(int? registrationNumber, bool? availeable, decimal? price)
    {
        var physios = _physioService.GetPhysios(registrationNumber, availeable, price);

        if (physios.Count == 0)
        {
            return NotFound();
        }

        return Ok(physios);
    }


    [HttpGet("{PhysioId}", Name = "GetPhysio")]
    public IActionResult GetPhysio(int PhysioId)
    {
        try
        {
            var physios = _physioService.GetPhysios(null, null, null);
            var physio = physios.FirstOrDefault(p => p.Value.PhysioId == PhysioId).Value;

            if (physio == null)
            {
                return NotFound();
            }

            return Ok(physio);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No se encontró el fisioterapeuta con número de registro " + PhysioId);
        }
    }


    [HttpPost]
    public IActionResult RegisterPhysio([FromBody] RegisterPhysioDTO physioDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _physioService.RegisterPhysio(
                name: physioDTO.Name,
                registrationNumber: physioDTO.RegistrationNumber,
                password: physioDTO.Password,
                availeable: physioDTO.Availeable,
                openingTime: physioDTO.OpeningTime,
                closingTime: physioDTO.ClosingTime,
                price: physioDTO.Price
            );

            return CreatedAtAction(nameof(SearchPhysio), new { registrationNumber = physioDTO.RegistrationNumber }, physioDTO);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{physioId}")]
    public IActionResult DeletePhysioById(int physioId)
    {
        try
        {
            var physios = _physioService.GetPhysios(null, null, null);
            var physio = physios.FirstOrDefault(p => p.Value.PhysioId == physioId).Value;

            if (physio == null)
            {
                return NotFound();
            }

            _physioService.DeletePhysio(physio);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el fisioterapeuta.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al procesar la solicitud.");
        }
    }


    [HttpPut("{physioId}")]
    public IActionResult UpdatePhysio(int physioId, [FromBody] UpdatePhysioDTO physioDTO)
    {
        try
        {
            var physios = _physioService.GetPhysios(null, null, null);
            var physio = physios.FirstOrDefault(p => p.Value.PhysioId == physioId).Value;

            if (physio == null)
            {
                return NotFound();
            }

            _physioService.UpdatePhysio(physio,
                password: physioDTO.Password,
                availeable: physioDTO.Availeable,
                openingTime: physioDTO.OpeningTime,
                closingTime: physioDTO.ClosingTime,
                price: physioDTO.Price
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
            _logger.LogError(ex, "Error al actualizar el fisioterapeuta.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al procesar la solicitud.");
        }
    }
}