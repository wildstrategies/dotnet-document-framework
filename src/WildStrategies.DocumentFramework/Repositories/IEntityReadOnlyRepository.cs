using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildStrategies.DocumentFramework
{
    public interface IEntityReadOnlyRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T?> GetAsync(Guid id);
        IQueryable<T> AsQueryable();
    }
}
