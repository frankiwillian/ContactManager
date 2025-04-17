using ContactManager.Application.DTOs;
using ContactManager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ContactDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _contactService.GetAllAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> Create(CreateContactDto createContactDto)
        {
            var contact = await _contactService.CreateAsync(createContactDto);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateContactDto updateContactDto)
        {
            try
            {
                await _contactService.UpdateAsync(id, updateContactDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contactService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}