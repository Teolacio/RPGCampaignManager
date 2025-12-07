using RPGCM.Domain.Abstration;

namespace RPGCM.Domain.Entities
{
    public class Grupo : EntityAbstration
    {
        public Grupo()
        {
        }

        public Grupo(Guid id, DateTime dataCriacao, string nome) : base(id, dataCriacao)
        {
            Nome = nome;
        }

        public string Nome {  get; set; }

        protected override IEnumerable<string> ValidateCustom()
        {
            if(string.IsNullOrWhiteSpace(Nome))
                yield return "Nome do grupo é obrigatório.";
        }
    }
}
