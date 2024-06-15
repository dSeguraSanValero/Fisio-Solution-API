using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPhysioRepository
{
    public IEnumerable<Physio> GetAllPhysios(int? registrationNumber, bool? availeable, decimal? price, string? sortBy, string? sortOrder);
    public void AddPhysio(Physio physio);
    public void RemovePhysio(Physio physio);
    public void UpdatePhysioDetails(Physio physio);
}