using FisioSolution.Data;
using FisioSolution.Models;


namespace FisioSolution.Business;
public class PhysioService : IPhysioService
{

    private readonly IPhysioRepository _repository;

    public PhysioService(IPhysioRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Dictionary<int, Physio> GetPhysios(int? registrationNumber, bool? availeable, decimal? price)
    {
        return _repository.GetAllPhysios(registrationNumber, availeable, price);
    }

    public void RegisterPhysio(string name, int registrationNumber, string password, bool availeable, TimeSpan horaApertura, TimeSpan horaCierre, decimal price)
    {
        try 
        {
            Physio physio = new(name, registrationNumber, password, availeable, horaApertura, horaCierre, price);
            _repository.AddPhysio(physio);
            _repository.SaveChanges();
        } 
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al registrar el usuario", e);
        }
    }




    public void UpdatePhysioTreatments(Physio physio, List<Treatment> treatments)
    {
        try
        {
            physio.MyTreatments = treatments;
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al actualizar los tratamientos del fisioterapeuta.", e);
        }
    }


}
