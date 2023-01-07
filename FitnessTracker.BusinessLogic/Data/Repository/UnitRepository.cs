using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.BusinessLogic.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly FitnessTrackerContext _fitnessTrackerContext;

        public UnitRepository(FitnessTrackerContext fitnessTrackerContext)
        {
            this._fitnessTrackerContext = fitnessTrackerContext;
        }

        public async Task<IEnumerable<Unit>> GetUnitsAsync()
        {
            return await _fitnessTrackerContext.Units.ToListAsync();
        }

        public async Task<Unit> GetUnitByIdAsync(int id)
        {
            Unit? unit = await _fitnessTrackerContext.Units.FindAsync(id);

            if (unit != null)
            {
                return unit;
            }

            return new Unit();
        }

        public async Task<Unit> AddUnitAsync(Unit unit)
        {
            await _fitnessTrackerContext.Units.AddAsync(unit);

            return unit;
        }

        public async Task DeleteUnitAsync(int id)
        {
            Unit? unit = await _fitnessTrackerContext.Units.FindAsync(id);

            if (unit != null)
            {
                _fitnessTrackerContext.Units.Remove(unit);
            }
        }
    }
}
