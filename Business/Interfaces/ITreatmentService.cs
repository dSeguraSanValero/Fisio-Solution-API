using FisioSolution.Models;

namespace FisioSolution.Business;

public interface ITreatmentService
{
    public void RegisterTreatment(int physioId, int patientId, string treatmentCause, DateTime treatmentDate, bool moreSessionsNeeded);
    public void DeleteTreatment(Treatment treatment);
    public Dictionary<int, Treatment> GetTreatments(int? physioId, int? patientId);
}