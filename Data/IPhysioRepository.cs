using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPhysioRepository
{
    void AddPhysio(Physio physio);
    void SaveChanges();
    Physio GetPhysio(int stringRegisterNumber);
    public Dictionary<int, Physio> GetAllPhysios(int? registrationNumber, bool? availeable, decimal? price);
}