using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPatientRepository
{

    void AddPatient(Patient patient);
    void SaveChanges();
    Patient GetPatient(string dni);
    public Dictionary<int, Patient> GetAllPatients();
    public void RemovePatient(Patient patient);

}