using FisioSolution.Models;

namespace FisioSolution.Data;
public interface IPhysioRepository
{
    public void AddPhysio(Physio physio);
    public void RemovePhysio(Physio physio);
    void SaveChanges();
    public void UpdatePhysioDetails(Physio physio);
    Physio GetPhysio(int stringRegisterNumber);
    public Dictionary<int, Physio> GetAllPhysios(int? registrationNumber, bool? availeable, decimal? price);
}