using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCM.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsValido();
        IEnumerable<string> GetValidationErrors();
    }
}
