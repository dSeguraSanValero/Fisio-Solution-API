using FisioSolution.Models;

namespace FisioSolution.Business;


public interface IPhysioService
{
    public void RegisterPhysio(string name, int registrationNumber, string password, bool availeable, TimeSpan horaApertura, TimeSpan horaCierre, decimal price);
    public void UpdatePhysioTreatments(Physio physio, List<Treatment> treatments);
    public Dictionary<int, Physio> GetPhysios(int? registrationNumber, bool? availeable, decimal? price);
}