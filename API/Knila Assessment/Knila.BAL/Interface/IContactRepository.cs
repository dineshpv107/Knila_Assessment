using Knila.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knila.BAL.Interface
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContact();

        Task AddOrUpdateContact(Contact contact);

        Task DeleteContact(Contact[] contacts);
    }
}
