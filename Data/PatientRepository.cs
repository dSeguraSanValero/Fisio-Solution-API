using FisioSolution.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FisioSolution.Data;

public class PatientRepository : IPatientRepository
{
    private readonly FisioSolutionContext _context;

    public PatientRepository(FisioSolutionContext context)
    {
        _context = context;
    }


    public void AddPatient(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
    }


    public void RemovePatient(Patient patient)
    {
        _context.Patients.Remove(patient);
        _context.SaveChanges();
    }


    public void UpdatePatientDetails(Patient patient)
    {
        _context.Patients.Update(patient);
        _context.SaveChanges();
    }


    public Dictionary<int, Patient> GetPatients(string? dni = null, bool? insurance = null)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrEmpty(dni))
        {
            query = query.Where(p => p.Dni == dni);
        }

        if (insurance.HasValue)
        {
            query = query.Where(p => p.Insurance == insurance.Value);
        }

        return query.ToDictionary(p => p.PatientId, p => p);
    }
}