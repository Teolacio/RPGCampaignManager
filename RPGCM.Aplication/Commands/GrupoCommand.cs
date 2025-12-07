using RPGCM.Aplication.Interfaces;
using RPGCM.Aplication.Shared;
using RPGCM.Domain.Entities;
using RPGCM.Domain.Interfaces;

namespace RPGCM.Aplication.Commands
{
    public class CriarGrupoCommand : ICommand<Guid>
    {
        public CriarGrupoCommand()
        {
        }

        public CriarGrupoCommand(Guid id, DateTime dataCriacao, string nome)
        {
            Id = id;
            DataCriacao = dataCriacao;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
    }

    public class CriarGrupoCommandHandler : ICommandHandler<CriarGrupoCommand, Guid>
    {
        private IRepository repository;

        public CriarGrupoCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> HandleAsync(CriarGrupoCommand command, CancellationToken cancellationToken)
        {
            repository.Add(ObjectMapper.Map<CriarGrupoCommand, Grupo>(command));
            return await Task.FromResult(command.Id);
        }
    }

    public class ExcluirGrupoCommand : ICommand<bool>
    {
        public Guid Id { get; set; }
        public DateTime DataExclusão { get; set; }
    }

    public class ExcluirGrupoCommandHandler : ICommandHandler<ExcluirGrupoCommand, bool>
    {
        private IRepository repository;

        public ExcluirGrupoCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> HandleAsync(ExcluirGrupoCommand command, CancellationToken cancellationToken)
        {
            var taskEntidade = await repository.Get(new EntityByIdSpecification<Grupo>(command.Id));
            var entidade = taskEntidade.ElementAt(0);
            entidade.DataExclusao = command.DataExclusão;
            repository.Update(entidade);
            return await Task.FromResult(true);
        }
    }

}
