using ContactManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}