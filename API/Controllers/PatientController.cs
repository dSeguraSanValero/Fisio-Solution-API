using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using FisioSolution.Data;
using FisioSolution.Business;
using FisioSolution.Models;
using System;

namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly MigrationDbContext _context;

    private readonly ILogger<PatientController> _logger;

    private readonly IPatientService _patientService;

    public PatientController(ILogger<PatientController> logger, IPatientService patientService, MigrationDbContext context)
    {
        _logger = logger;
        _patientService = patientService;
        _context = context;
    }



    [HttpGet(Name = "GetAllPatients")]
    public ActionResult<Dictionary<string, Patient>> SearchPatient(string? Dni, bool? insurance)
    {
        var patientsDictionary = _patientService.GetPatients();
        var query = patientsDictionary.AsQueryable();

        if (insurance.HasValue)
        {
            query = query.Where(kvp => kvp.Value.Insurance == insurance.Value);
        }

        var patients = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        if (patients.Count == 0)
        {
            return NotFound();
        }

        return Ok(patients);
    }




    [HttpGet("{PatientId}")]
    public IActionResult GetPatient(int PatientId)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.PatientId == PatientId);

        if (patient == null)
        {
            return NotFound();
        }

        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        return Ok(patient);
    }


    [HttpPost]
    public IActionResult CreatePatient(Patient patient)
    {
        try
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetPatient), new { patientId = patient.PatientId }, patient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpOptions]
    public IActionResult Options()
    {
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

        return Ok();
    }

}