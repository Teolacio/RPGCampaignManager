using RPGCM.Aplication.Commands;
using RPGCM.Aplication.Shared;
using RPGCM.TDD.Shared;

namespace RPGCM.TDD
{
    public class CampanhaTesteUnitario
    {
        [Fact]
        public async Task Criar_uma_campanha_e_confirmar_se_está_gravado()
        {
            Guid idGrupo = Guid.NewGuid();
            var factory = new Factory();
            var handler = factory.CreateCriarGrupoCommandHandler();
            await handler.HandleAsync(new CriarGrupoCommand(idGrupo, DateTime.Now, "Nome Grupo 1"), new CancellationToken());


        }

    }
}
