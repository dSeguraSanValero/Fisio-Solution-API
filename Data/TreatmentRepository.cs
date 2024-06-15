using FisioSolution.Models;
using System.Text.Json;

namespace FisioSolution.Data;

public class TreatmentRepository : ITreatmentRepository
{
    private readonly FisioSolutionContext _context;
    public TreatmentRepository(FisioSolutionContext context)
    {
        _context = context;
    }

    public Dictionary<int, Treatment> GetAllTreatments(int? physioId, int? patientId)
    {
        var query = _context.Treatments.AsQueryable();

        if (physioId.HasValue)
        {
            query = query.Where(p => p.PhysioId == physioId);
        }

        if (patientId.HasValue)
        {
            query = query.Where(p => p.PatientId == patientId.Value);
        }

        return query.ToDictionary(p => p.TreatmentId, p => p);
    }

        
    public void AddTreatment(Treatment treatment)
    {
        _context.Treatments.Add(treatment);
        _context.SaveChanges();
    }


    public void RemoveTreatment(Treatment treatment)
    {
        _context.Treatments.Remove(treatment);
        _context.SaveChanges();
    }
}