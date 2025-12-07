namespace RPGCM.Domain.Shared
{
    public class InvalidEntityException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public InvalidEntityException(string message)
            : base(message)
        {
            Errors = Enumerable.Empty<string>();
        }

        public InvalidEntityException(IEnumerable<string> errors)
            : base("A entidade é inválida.")
        {
            Errors = errors;
        }
    }
}
