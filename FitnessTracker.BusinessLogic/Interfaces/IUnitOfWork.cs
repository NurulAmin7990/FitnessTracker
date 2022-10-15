namespace FitnessTracker.BusinessLogic.Interfaces
{
    public interface IUnitOfWork
    {
        IUnitRepository UnitRepository { get; }

        Task<bool> SaveAsync();
    }
}
