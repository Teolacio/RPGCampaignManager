using RPGCM.Aplication.Commands;
using RPGCM.Aplication.Shared;
using RPGCM.Domain.Entities;
using RPGCM.Domain.Shared;
using RPGCM.TDD.Shared;

namespace RPGCM.TDD
{
    public class GrupoTesteUnitario
    {
        [Fact]
        public async Task Criar_um_grupo_e_confirmar_se_está_gravado()
        {
            Guid id = Guid.NewGuid();
            DateTime dateInicio = DateTime.Now;
            var factory = new Factory();
            var handler = factory.CreateCriarGrupoCommandHandler();

            await handler.HandleAsync(new CriarGrupoCommand(id, dateInicio, "Nome Grupo 1"), new CancellationToken());
            await handler.HandleAsync(new CriarGrupoCommand(Guid.NewGuid(), new DateTime(2004, 12, 3), "nomeGrupo2"), new CancellationToken());
            await handler.HandleAsync(new CriarGrupoCommand(Guid.NewGuid(), new DateTime(2004, 1, 13), "nomeGrupo4"), new CancellationToken());

            var resultado = await factory.Repositorio.Get(new EntityByIdSpecification<Grupo>(id));

            Assert.True(resultado.Count() == 1);
            Assert.Equivalent(new Grupo(id, dateInicio, "Nome Grupo 1"), resultado.ElementAt(0));
        }

        [Fact]
        public async Task Esta_invalido_na_hora_de_criar()
        {
            Guid id = Guid.NewGuid();
            DateTime dateInicio = DateTime.Now;
            var repositorio = new RepositorioTeste();
            var decorator = new ValidatingRepositoryDecorator(repositorio);
            var handler = new CriarGrupoCommandHandler(decorator);
            var comandoSemNome = new CriarGrupoCommand()
            {
                Nome = null,
                Id = id,
                DataCriacao = dateInicio
            };
            var comandoSemId = new CriarGrupoCommand()
            {
                Nome = "Nome do Grupo",
                DataCriacao = dateInicio
            };
            var comandoSemDataCriacao = new CriarGrupoCommand()
            {
                Nome = "Nome do Grupo",
                Id = id
            };

            var ex1 = Assert.ThrowsAsync<InvalidEntityException>(() => handler.HandleAsync(comandoSemNome, new CancellationToken()));
            var ex2 = Assert.ThrowsAsync<InvalidEntityException>(() => handler.HandleAsync(comandoSemId, new CancellationToken()));
            var ex3 = Assert.ThrowsAsync<InvalidEntityException>(() => handler.HandleAsync(comandoSemDataCriacao, new CancellationToken()));

            Assert.Equal("A entidade é inválida.", ex1.Result.Message);
            Assert.Equal("Nome do grupo é obrigatório.", ex1.Result.Errors.ElementAt(0));
            Assert.NotEmpty(ex1.Result.Errors);

            Assert.Equal("A entidade é inválida.", ex2.Result.Message);
            Assert.Equal("Id não pode ser vazio.", ex2.Result.Errors.ElementAt(0));
            Assert.NotEmpty(ex2.Result.Errors);

            Assert.Equal("A entidade é inválida.", ex3.Result.Message);
            Assert.Equal("Data de criação não pode ser vazio.", ex3.Result.Errors.ElementAt(0));
            Assert.NotEmpty(ex3.Result.Errors);
        }

        [Fact]
        public async Task Exclusao_logica()
        {
            Guid id = Guid.NewGuid();
            DateTime DataAgora = DateTime.Now;
            var repositorio = new RepositorioTeste();
            var decorator = new ValidatingRepositoryDecorator(repositorio);
            var handler = new CriarGrupoCommandHandler(decorator);
            var comando1 = new CriarGrupoCommand()
            {
                Nome = "Nome Grupo 1",
                Id = id,
                DataCriacao = new DateTime(2020, 12, 01)
            };
            await handler.HandleAsync(comando1, new CancellationToken());
            var resultado = await repositorio.Get(new EntityByIdSpecification<Grupo>(id));
            Assert.True(resultado.Count() == 1);

            var handlerExcluir = new ExcluirGrupoCommandHandler(decorator);
            var comandoExcluir = new ExcluirGrupoCommand()
            {
                Id = id,
                DataExclusão = DataAgora,
            };

            await handlerExcluir.HandleAsync(comandoExcluir, new CancellationToken());
            var resultadoExcluir = await repositorio.Get(new EntityByIdSpecification<Grupo>(id));
            Assert.True(resultadoExcluir.Count() == 1);
            Assert.True(resultadoExcluir.ElementAt(0).DataExclusao == DataAgora);
        }

    }
}
