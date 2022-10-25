using Contacts.Data;
using Contacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Service
{
    public interface IContactsService
    {
        List<Contact>  GetContacts();
        Task<List<Contact>> DeleteContact(int id);

        Task<List<Contact>> UpdateContact(Contact cntct);
        Task<List<Contact>> CreateContacts(Contact contact);
        Task<Contact> FindContact(int id);

    }
    public class ContactsService : IContactsService
    {
        private readonly ContactsDbContext _context;

        public ContactsService(ContactsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contact>> CreateContacts(Contact contact)
        {
            var res = ConvertdbContactToContact(contact);
            _context.contacts.Add(res);
            await _context.SaveChangesAsync();
            return ConvertdbContactToContact(_context.contacts.ToList()).ToList();
        }

        private dbContact ConvertdbContactToContact(Contact contact)
        {
            return new dbContact()
            {

                Name = contact.Name,
                Address = contact.Address,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };
        }
        private Contact ConvertdbContactToContact(dbContact contact)
        {
            return new Contact()
            {
                Id  = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };
        }
        private IEnumerable<Contact> ConvertdbContactToContact(List<dbContact> contacts)
        {
            foreach (dbContact contact in contacts)
            yield return new Contact()
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };
        }

        public async Task<List<Contact>> DeleteContact(int id)
        {
            var dbContact = await _context.contacts.FindAsync(id);
            
            _context.contacts.Remove(dbContact);
            await _context.SaveChangesAsync();

            return ConvertdbContactToContact(_context.contacts.ToList()).ToList();
        }

        public List<Contact> GetContacts()
        {
            return ConvertdbContactToContact(_context.contacts.ToList()).ToList();
        }

        public async Task<List<Contact>> UpdateContact(Contact cntct)
        {
            var dbcnt = await _context.contacts.FindAsync(cntct.Id);


            dbcnt.Name = cntct.Name;
            dbcnt.FirstName = cntct.FirstName;
            dbcnt.LastName = cntct.LastName;
            dbcnt.Address = cntct.Address;

            await _context.SaveChangesAsync();

            return  ConvertdbContactToContact(_context.contacts.ToList()).ToList();
        }

        public async Task<Contact> FindContact(int id)
        {
            var res = await _context.contacts.FindAsync(id);
            if (res == null)
            {
                return null;
            }
            return  ConvertdbContactToContact(res);
        }

      
    }
}
