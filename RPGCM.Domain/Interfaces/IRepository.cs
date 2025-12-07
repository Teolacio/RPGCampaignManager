namespace RPGCM.Domain.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : IEntity;

        Task<IEnumerable<T>> Get<T>(ISpecification<T> specification) where T : IEntity;

        Task<bool> Update<T>(T entity) where T : IEntity;
    }
}
