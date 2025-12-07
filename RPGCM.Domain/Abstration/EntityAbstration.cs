using RPGCM.Domain.Interfaces;

namespace RPGCM.Domain.Abstration
{
    public abstract class EntityAbstration : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataExclusao { get; set; }
        internal bool IsValid;

        protected EntityAbstration(Guid id, DateTime dataCriacao)
        {
            Id = id;
            DataCriacao = dataCriacao;
        }

        protected EntityAbstration()
        {
        }

        public bool IsValido()
        {
            return !GetValidationErrors().Any();
        }

        public virtual IEnumerable<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if(Id == Guid.Empty)
                errors.Add("Id não pode ser vazio.");

            if(DataCriacao == null || DataCriacao == DateTime.MinValue)
                errors.Add("Data de criação não pode ser vazio.");

            errors.AddRange(ValidateCustom());

            return errors;
        }
        protected abstract IEnumerable<string> ValidateCustom();
    }
}
