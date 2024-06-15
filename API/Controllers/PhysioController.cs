using Microsoft.AspNetCore.Mvc;
using FisioSolution.Data;
using FisioSolution.Models;
using FisioSolution.Business;

namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhysioController : ControllerBase
{
    private readonly MigrationDbContext _context;

    private readonly ILogger<PatientController> _logger;

    private readonly IPhysioService _physioService;


    public PhysioController(MigrationDbContext context, ILogger<PhysioController> logger, IPhysioService physioService)
    {
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


    [HttpDelete("{PhysioId}")]
    public IActionResult DeletePhysio(int PhysioId)
    {
        var physio = _context.Physios.FirstOrDefault(p => p.PhysioId == PhysioId);

        if (physio == null)
        {
            return NotFound();
        }

        try
        {
            _context.Physios.Remove(physio);
            _context.SaveChanges();

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}