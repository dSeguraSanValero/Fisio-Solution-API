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
        try
        {
            _repository.RemovePatient(patient);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al borrar el usuario", e);
        }
    }

    public void UpdatePatientData(Patient patient, string newName, string newPassword, decimal newWeight, decimal newHeight, bool newInsurance)
    {
        try
        {
            if (patient != null)
            {
                patient.Name = newName;
                patient.Password = newPassword;
                patient.Weight = newWeight;
                patient.Height = newHeight;
                patient.Insurance = newInsurance;

                _repository.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(patient), "El paciente proporcionado es nulo.");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al actualizar los datos del paciente.", e);
        }
    }

    public Dictionary<int, Patient> GetPatients(string? dni = null, bool? insurance = null)
    {
        return _repository.GetPatients(dni, insurance);
    }

}