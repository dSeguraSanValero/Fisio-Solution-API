using FisioSolution.Data;
using FisioSolution.Models;


namespace FisioSolution.Business;

public class TreatmentService : ITreatmentService
{

    private readonly ITreatmentRepository _repository;

    public TreatmentService(ITreatmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }


    public IEnumerable<Treatment> GetTreatments(int? physioId, int? patientId)
    {
        return _repository.GetAllTreatments(physioId, patientId);
    }


    public void RegisterTreatment(int physioId, int patientId, string treatmentCause, DateTime treatmentDate, bool moreSessionsNeeded)
    {
        var newTreatment = new Treatment
        {
            PhysioId = physioId,
            PatientId = patientId,
            TreatmentCause = treatmentCause,
            TreatmentDate = treatmentDate,
            MoreSessionsNeeded = moreSessionsNeeded
        };

        _repository.AddTreatment(newTreatment);
    }


    public void DeleteTreatment(Treatment treatment)
    {
        _repository.RemoveTreatment(treatment);
    }
}
