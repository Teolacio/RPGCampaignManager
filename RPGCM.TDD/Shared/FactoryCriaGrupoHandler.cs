using RPGCM.Aplication.Commands;
using RPGCM.Aplication.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCM.TDD.Shared
{
    internal class Factory
    {
        internal RepositorioTeste Repositorio;
        public Factory()
        {
            Repositorio = new RepositorioTeste();
        }

        internal CriarGrupoCommandHandler CreateCriarGrupoCommandHandler()
        {
            var decorator = new ValidatingRepositoryDecorator(Repositorio);
            var handler = new CriarGrupoCommandHandler(decorator);
            return handler;
        }
    }
}
