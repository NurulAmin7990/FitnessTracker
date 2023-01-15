using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using FitnessTracker.BusinessLogic.Repository;

namespace FitnessTracker.BusinessLogic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitnessTrackerContext _dataContext;

        public UnitOfWork(FitnessTrackerContext fitnessTrackerContext)
        {
           this._dataContext = fitnessTrackerContext;
        }

        public IUnitRepository UnitRepository => 
            new UnitRepository(_dataContext);

        public async Task<bool> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
