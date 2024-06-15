using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPatientRepository
{

    void AddPatient(Patient patient);
    Patient GetPatient(string dni);
    public Dictionary<int, Patient> GetPatients(string? dni = null, bool? insurance = null);
    public void RemovePatient(Patient patient);
    public void UpdatePatientDetails(Patient patient);

}