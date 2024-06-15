using FisioSolution.Models;

namespace FisioSolution.Business;

public interface IPatientService
{
    public void RegisterPatient(string name, string dni, string password, DateTime birthDate, decimal weight, decimal height, bool insurance);
    public void DeletePatient(Patient patient);
    public void UpdatePatient(Patient patient, string password, decimal weight, decimal height, bool insurance);
    public Dictionary<int, Patient> GetPatients(string? dni = null, bool? insurance = null);
}