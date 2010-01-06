using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ContactManager.Models
{
    public interface IContactManagerService
    {
        bool CreateContact(Contact contactToCreate);
        bool DeleteContact(Contact contactToDelete);
        bool EditContact(Contact contactToEdit);
        Contact GetContact(int id);
        IEnumerable<Contact> ListContacts();
    }
}
