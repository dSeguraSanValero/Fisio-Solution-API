using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPhysioRepository
{
    public void AddPhysio(Physio physio);
    public void RemovePhysio(Physio physio);
    public void UpdatePhysioDetails(Physio physio);
    public IEnumerable<Physio> GetAllPhysios(int? registrationNumber, bool? availeable, decimal? price, string? sortBy);
}