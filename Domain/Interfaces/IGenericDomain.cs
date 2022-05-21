using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericDomain<T> where T : class
    {
        Task<T> GetByID(long id);
        Task<IEnumerable<T>> GetAll();
        
    }
}
