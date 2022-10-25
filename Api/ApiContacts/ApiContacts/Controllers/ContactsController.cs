using Contacts.Domain;
using Contacts.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiContacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService service;

        public ContactsController(IContactsService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts()
        {
            return Ok(await service.GetContacts());
        }

        [HttpPost]
        public async Task<ActionResult<List<Contact>>> CreateContacts(Contact contact)
        {
            //_context.SuperHeroes.Add(hero);
            //await _context.SaveChangesAsync();            //_context.SuperHeroes.Add(hero);
            //await _context.SaveChangesAsync();

            return Ok(await service.CreateContacts(contact));
        }

        [HttpPut]
        public async Task<ActionResult<List<Contact>>> UpdateContact(Contact cntct)
        {
            var dbContacts = await service.UpdateContact(cntct);
           

            return Ok( dbContacts);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Contact>>> DeleteContact(int id)
        {
            

            return Ok(await service.DeleteContact(id));
        }
    }
}

