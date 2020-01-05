using FootballParser.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballParser.Domain.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Get(string id);

        Task<IEnumerable<T>> GetAll();

        Task Add(T item);

        Task Add(IEnumerable<T> entity);

        Task Delete(T entity);

        Task Update(T entity);

    }
}
