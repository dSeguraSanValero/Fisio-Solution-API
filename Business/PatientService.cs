using FisioSolution.Data;
using FisioSolution.Models;

namespace FisioSolution.Business;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }


    public IEnumerable<Patient> GetPatients(string? dni = null, bool? insurance = null)
    {
        return _repository.GetAllPatients(dni, insurance);
    }


    public void RegisterPatient(string name, string dni, string password, DateTime birthDate, decimal weight, decimal height, bool insurance)
    {

        var newPatient = new Patient
        {
            Name = name,
            Dni = dni,
            Password = password,
            BirthDate = birthDate.Date,
            Weight = weight,
            Height = height,
            Insurance = insurance
        };

        _repository.AddPatient(newPatient);
    }


    public void DeletePatient(Patient patient)
    {
        _repository.RemovePatient(patient);
    }


    public void UpdatePatient(Patient patient, string password, decimal weight, decimal height, bool insurance)
    {
        patient.Password = password;
        patient.Insurance = insurance;
        patient.Weight = weight;
        patient.Height = height;

        _repository.UpdatePatientDetails(patient);
    }
}