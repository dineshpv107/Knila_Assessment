using Knila.BAL.Interface;
using Knila.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Knila_Assessment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactRepository;
        public ContactController(IContactRepository userRepository, IConfiguration configuration)
        {
                this.contactRepository = userRepository;
        }

        [HttpPost]
        public async Task AddOrUpdateContact([FromBody]Contact contact)
        {
            await this.contactRepository.AddOrUpdateContact(contact);
        }

        [HttpGet]
        public async Task<List<Contact>> GetAllContacts()
        {
            return await this.contactRepository.GetAllContact();
        }

        [HttpPost]
        public async Task RemoveContacts([FromBody] Contact[] contacts)
        {
            await this.contactRepository.DeleteContact(contacts);
        }
    }
}
