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
    public ActionResult<Dictionary<int, Patient>> SearchPatient(string? dni, bool? insurance)
    {
        var patients = _patientService.GetPatients(dni, insurance);

        if (patients.Count == 0)
        {
            return NotFound();
        }

        return Ok(patients);
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

}