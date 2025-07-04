using Knila.BAL.Interface;
using Knila.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Knila.BAL.Implementations
{
    public class ContactRepository : IContactRepository
    {

        private readonly KnilaDbContext _context;
        private readonly ILogger<ContactRepository> _logger;
        public ContactRepository(ILogger<ContactRepository> logger,KnilaDbContext context) 
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Contact>> GetAllContact()
        {
            try
            {
                return await _context.Contacts.OrderByDescending(contact=>contact.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error Contact List");
            }
        }

        public async Task AddOrUpdateContact(Contact contact)
        {
            try
            {
                if (contact.Id == 0)
                {
                    await _context.Contacts.AddAsync(contact);
                }
                else
                {
                    _context.Contacts.Update(contact);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error In Contact creation");
            }
        }

        public async Task DeleteContact(Contact[] contacts)
        {
            try
            {
                _context.Contacts.RemoveRange(contacts);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Error In Contact Deletion");
            }
        }
    }
}
