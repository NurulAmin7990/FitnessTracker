using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using FitnessTracker.WebAPI.ViewModels;

namespace FitnessTracker.WebAPI.Queries
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<ViewUnitVM>> GetUnits([Service] IUnitOfWork uow)
        {
            IEnumerable<Unit> units = await uow.UnitRepository.GetUnitsAsync();
            List<ViewUnitVM> UnitsVMs = new List<ViewUnitVM>();

            foreach (Unit unit in units)
            {
                UnitsVMs.Add(new ViewUnitVM
                {
                    Id = unit.Id,
                    UnitType = unit.UnitType,
                    Description = unit.Description
                });
            }

            return UnitsVMs.AsQueryable();
        }
    }
}
