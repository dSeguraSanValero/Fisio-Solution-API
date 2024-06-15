using FisioSolution.Models;
using System.Text.Json;

namespace FisioSolution.Data;

public class PhysioRepository : IPhysioRepository
{
    private Dictionary<string, Physio> _physios = new Dictionary<string, Physio>();
    private readonly string _filePath = "physios.json";
    private readonly FisioSolutionContext _context;

    public PhysioRepository(FisioSolutionContext context)
    {
        _context = context;
    }    

    public PhysioRepository()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var physios = JsonSerializer.Deserialize<IEnumerable<Physio>>(jsonString);
                _physios = physios.ToDictionary(acc => acc.PhysioId.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Ha ocurrido un error al leer el archivo de usuarios", e);
            }
        }

        if (_physios.Count == 0)
        {
            Physio.PhysioIdSeed = 1;
        }
        else
        {
            Physio.PhysioIdSeed = _physios.Count + 1;
        }
    }

    public void AddPhysio(Physio physio)
    {
        _context.Physios.Add(physio);
        _context.SaveChanges();
    }

    
    public Physio GetPhysio(int registrationNumber)
    {
        foreach (var physio in _physios.Values)
        {
            if (physio.RegistrationNumber == registrationNumber)
            {
                return physio;
            }
        }
        return null;
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

    public void RemovePhysio(Physio physio)
    {
        _context.Physios.Remove(physio);
        _context.SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_physios.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al guardar cambios en el archivo de usuarios", e);
        }
    }

    public void UpdatePhysioDetails(Physio physio)
    {
        _context.Physios.Update(physio);
        _context.SaveChanges();
    }

}



