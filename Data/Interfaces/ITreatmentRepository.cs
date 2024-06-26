using FisioSolution.Models;

namespace FisioSolution.Data;
public interface ITreatmentRepository
{
    public IEnumerable<Treatment> GetAllTreatments(int? physioId, int? patientId);

    public void AddTreatment(Treatment treatment);

    public void RemoveTreatment(Treatment treatment);
}