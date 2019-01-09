using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.MVC.Models;

namespace Airport.MVC.Services
{
    public interface IFlightRepository
    {
        Task<IEnumerable<FlightViewModel>> GetAllFlights();
        Task<IEnumerable<FlightViewModel>> GetFlight(int id);
        Task AddFlight(FlightViewModel flight);
        Task <bool>UpdateFlight(int id);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<FlightViewModel>> SearchFlight(FlightViewModel flight);
    }
}