using FisioSolution.Models;
using System.Text.Json;

namespace FisioSolution.Data;

public class PhysioRepository : IPhysioRepository
{
    private readonly FisioSolutionContext _context;

    public PhysioRepository(FisioSolutionContext context)
    {
        _context = context;
    }    


    public void AddPhysio(Physio physio)
    {
        _context.Physios.Add(physio);
        _context.SaveChanges();
    }


    public void RemovePhysio(Physio physio)
    {
        _context.Physios.Remove(physio);
        _context.SaveChanges();
    }


    public void UpdatePhysioDetails(Physio physio)
    {
        _context.Physios.Update(physio);
        _context.SaveChanges();
    }


    public Dictionary<int, Physio> GetAllPhysios(int? registrationNumber, bool? availeable, decimal? price)
    {
        var query = _context.Physios.AsQueryable();

        if (registrationNumber.HasValue)
        {
            query = query.Where(p => p.RegistrationNumber == registrationNumber);
        }

        if (availeable.HasValue)
        {
            query = query.Where(p => p.Availeable == availeable.Value);
        }

        if (price.HasValue)
        {
            query = query.Where(p => p.Price == price.Value);
        }

        return query.ToDictionary(p => p.PhysioId, p => p);
    }

}



