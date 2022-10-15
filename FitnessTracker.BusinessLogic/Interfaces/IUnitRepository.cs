using FitnessTracker.BusinessLogic.Models;

namespace FitnessTracker.BusinessLogic.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetUnitsAsync();

        Task<Unit> GetUnitByIdAsync(int id);

        Task AddUnitAsync(Unit unit);

        Task DeleteUnitAsync(int unitId);
    }
}
