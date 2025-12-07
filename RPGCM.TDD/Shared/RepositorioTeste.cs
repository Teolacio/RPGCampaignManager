using RPGCM.Domain.Interfaces;
using RPGCM.Domain.Shared;

namespace RPGCM.TDD.Shared
{
    internal class RepositorioTeste : IRepository
    {
        List<IEntity> listaSimulandoBanco = new List<IEntity>();

        public void Add<T>(T entity) where T : IEntity
        {
            if(!entity.IsValido())
                throw new InvalidEntityException(entity.GetValidationErrors());

            listaSimulandoBanco.Add(entity);
        }

        public Task<IEnumerable<T>> Get<T>(ISpecification<T> specification) where T : IEntity
        {
            var listaTipada = listaSimulandoBanco.OfType<T>();

            var resultado = listaTipada
                .Where(specification.Criteria.Compile())
                .AsEnumerable();

            return Task.FromResult(resultado);
        }

        public Task<bool> Update<T>(T entity) where T : IEntity
        {
            var listaTipada = listaSimulandoBanco.OfType<T>();
            var alterar = listaTipada.Where(x => x.Id == entity.Id).First();
            alterar = entity;
            return Task.FromResult(true); ;
        }
    }
}
