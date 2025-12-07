using RPGCM.Domain.Interfaces;
using RPGCM.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCM.Aplication.Shared
{
    public class ValidatingRepositoryDecorator : IRepository
    {
        private readonly IRepository _inner;

        public ValidatingRepositoryDecorator(IRepository inner)
        {
            _inner = inner;
        }

        public void Add<T>(T entity) where T : IEntity
        {
            if(entity.GetValidationErrors().Any())
                throw new InvalidEntityException(entity.GetValidationErrors());

            _inner.Add(entity);
        }

        public Task<IEnumerable<T>> Get<T>(ISpecification<T> specification) where T : IEntity
        {
            return _inner.Get<T>(specification);
        }

        public Task<bool> Update<T>(T entity) where T : IEntity
        {
            if(entity.GetValidationErrors().Any())
                throw new InvalidEntityException(entity.GetValidationErrors());

            return _inner.Update(entity);
        }
    }
}
