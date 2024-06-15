using FisioSolution.Models;

namespace FisioSolution.Business;

public interface IPhysioService
{
    public Dictionary<int, Physio> GetPhysios(int? registrationNumber, bool? availeable, decimal? price);
    public void RegisterPhysio(string name, int registrationNumber, string password, bool availeable, TimeSpan openingTime, TimeSpan closingTime, decimal price);
    public void UpdatePhysioTreatments(Physio physio, List<Treatment> treatments);
    public void DeletePhysio(Physio physio);
    public void UpdatePhysio(Physio physio, string password, TimeSpan openingTime, TimeSpan closingTime, bool availeable, decimal price);
}