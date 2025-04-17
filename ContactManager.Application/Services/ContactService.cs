using ContactManager.Application.DTOs;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<PaginatedResult<ContactDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var contacts = await _contactRepository.GetAllAsync(page, pageSize);
            var totalCount = await _contactRepository.GetTotalCountAsync();

            return new PaginatedResult<ContactDto>
            {
                Items = contacts.Select(c => new ContactDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }),
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<ContactDto?> GetByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
                return null;

            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                CreatedAt = contact.CreatedAt,
                UpdatedAt = contact.UpdatedAt
            };
        }

        public async Task<ContactDto> CreateAsync(CreateContactDto createContactDto)
        {
            var contact = new Contact
            {
                FirstName = createContactDto.FirstName,
                LastName = createContactDto.LastName,
                PhoneNumber = createContactDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            await _contactRepository.AddAsync(contact);

            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                CreatedAt = contact.CreatedAt,
                UpdatedAt = contact.UpdatedAt
            };
        }

        public async Task UpdateAsync(int id, UpdateContactDto updateContactDto)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
                throw new KeyNotFoundException($"Contact with ID {id} not found.");

            contact.FirstName = updateContactDto.FirstName;
            contact.LastName = updateContactDto.LastName;
            contact.PhoneNumber = updateContactDto.PhoneNumber;
            contact.UpdatedAt = DateTime.UtcNow;

            await _contactRepository.UpdateAsync(contact);
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
                throw new KeyNotFoundException($"Contact with ID {id} not found.");

            await _contactRepository.DeleteAsync(id);
        }
    }
}