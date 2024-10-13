using Contact_Management.ContactRepository;
using Contact_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Contact_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public ILogger<ContactsController> _logger;
        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger= logger;
        }

        [HttpGet]
        public ActionResult<List<Contact>> Getcontacts()
        {
                return ContactDbHelper.ReadFromJsonFile<Contact>();
                   
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> Getcontact(int id)
        {
                var contacts = ContactDbHelper.ReadFromJsonFile<Contact>();
                var contactFirst = contacts.FirstOrDefault(p => p.Id == id);
                return contactFirst;
 
        }

        [HttpPost]
        public ActionResult<List<Contact>> CreateContact([FromBody] Contact newContact)
        {
            var contacts = ContactDbHelper.ReadFromJsonFile<Contact>();
            var MaxId= contacts.OrderByDescending(p => p.Id).FirstOrDefault();
            newContact.Id = MaxId!=null ? Convert.ToInt32(MaxId.Id) + 1 : 1;
            contacts.Add(newContact);
            ContactDbHelper.WriteToJsonFile(contacts);
            return ContactDbHelper.ReadFromJsonFile<Contact>();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Contact>> UpdateContact(int id, [FromBody] Contact updatedContact)
        {
            var contacts = ContactDbHelper.ReadFromJsonFile<Contact>();
            var contact = contacts.FirstOrDefault(p => p.Id == id);

            contact.FirstName = updatedContact.FirstName;
            contact.LastName = updatedContact.LastName;
            contact.Email = updatedContact.Email;
            ContactDbHelper.WriteToJsonFile(contacts);

            return ContactDbHelper.ReadFromJsonFile<Contact>();
        }

        [HttpDelete("{id}")]
        public ActionResult<List<Contact>> DeleteContact(int id)
        {
            var contacts = ContactDbHelper.ReadFromJsonFile<Contact>();
            var contact = contacts.FirstOrDefault(p => p.Id == id);
            contacts.Remove(contact);
            ContactDbHelper.WriteToJsonFile(contacts);

            return ContactDbHelper.ReadFromJsonFile<Contact>();
        }
    }
}
