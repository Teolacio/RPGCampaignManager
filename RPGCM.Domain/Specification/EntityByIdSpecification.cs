using RPGCM.Domain.Interfaces;
using System.Linq.Expressions;

public class EntityByIdSpecification<T> : ISpecification<T>
    where T : class, IEntity
{
    public Guid Id { get; }

    public EntityByIdSpecification(Guid id)
    {
        Id = id;
    }

    public Expression<Func<T, bool>> Criteria => x => x.Id == Id;

    public List<Expression<Func<T, object>>> Includes => throw new NotImplementedException();

    public Expression<Func<T, object>>? OrderBy => throw new NotImplementedException();

    public Expression<Func<T, object>>? OrderByDescending => throw new NotImplementedException();

    public int? Take => throw new NotImplementedException();

    public int? Skip => throw new NotImplementedException();

    public bool IsPagingEnabled => throw new NotImplementedException();
}
