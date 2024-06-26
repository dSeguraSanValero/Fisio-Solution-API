using FisioSolution.Models;

namespace FisioSolution.Business;

public interface IPhysioService
{
    public IEnumerable<Physio> GetPhysios(int? registrationNumber, bool? availeable, decimal? price, string? sortBy, string? sortOrder);
    public void RegisterPhysio(string name, int registrationNumber, string password, bool availeable, TimeSpan openingTime, TimeSpan closingTime, decimal price);
    public void UpdatePhysio(Physio physio, string password, TimeSpan openingTime, TimeSpan closingTime, bool availeable, decimal price);
    public void DeletePhysio(Physio physio);
}