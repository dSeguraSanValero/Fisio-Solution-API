using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPatientRepository
{
    public IEnumerable<Patient> GetAllPatients(string? dni = null, bool? insurance = null);
    void AddPatient(Patient patient);
    public void RemovePatient(Patient patient);
    public void UpdatePatientDetails(Patient patient);
}