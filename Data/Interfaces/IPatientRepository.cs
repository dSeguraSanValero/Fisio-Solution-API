using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPatientRepository
{
    void AddPatient(Patient patient);
    public void RemovePatient(Patient patient);
    public void UpdatePatientDetails(Patient patient);
    public Dictionary<int, Patient> GetPatients(string? dni = null, bool? insurance = null);
}