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


    public void RegisterPhysio(string name, int registrationNumber, string password, bool availeable, TimeSpan openingTime, TimeSpan closingTime, decimal price)
    {

        var newPhysio = new Physio
        {
            Name = name,
            RegistrationNumber = registrationNumber,
            Password = password,
            Availeable = availeable,
            OpeningTime = openingTime,
            ClosingTime = closingTime,
            Price = price
        };

        _repository.AddPhysio(newPhysio);
    }


    public void DeletePhysio(Physio physio)
    {
        _repository.RemovePhysio(physio);
    }


    public void UpdatePhysio(Physio physio, string password, TimeSpan openingTime, TimeSpan closingTime, bool availeable, decimal price)
    {
        physio.Password = password;
        physio.OpeningTime = openingTime;
        physio.ClosingTime = closingTime;
        physio.Availeable = availeable;
        physio.Price = price;

        _repository.UpdatePhysioDetails(physio);
    }

    
    public IEnumerable<Physio> GetPhysios(int? registrationNumber, bool? availeable, decimal? price, string? sortBy)
    {
        return _repository.GetAllPhysios(registrationNumber, availeable, price, sortBy);
    }
    }
