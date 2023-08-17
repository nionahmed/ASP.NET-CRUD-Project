using CRUDFunctionality.api.DbContexts;
using CRUDFunctionality.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Net;
using System.Numerics;

namespace CRUDFunctionality.api.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]


    public class ContactsController : Controller
    {
        private ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                id = Guid.NewGuid(),
                name = addContactRequest.Name,
                address = addContactRequest.Address,
                email = addContactRequest.Email,
                phone = addContactRequest.Phone,
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.name = updateContactRequest.name;
                contact.phone = updateContactRequest.phone;
                contact.address = updateContactRequest.address;
                contact.email = updateContactRequest.email;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChanges();
                return Ok(contact);
            }
            return NotFound();
        }

    }
    


    
}
