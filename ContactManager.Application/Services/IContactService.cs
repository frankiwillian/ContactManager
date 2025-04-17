using ContactManager.Application.DTOs;
using System.Threading.Tasks;

namespace ContactManager.Application.Services
{
    public interface IContactService
    {
        Task<PaginatedResult<ContactDto>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<ContactDto?> GetByIdAsync(int id);
        Task<ContactDto> CreateAsync(CreateContactDto createContactDto);
        Task UpdateAsync(int id, UpdateContactDto updateContactDto);
        Task DeleteAsync(int id);
    }
}