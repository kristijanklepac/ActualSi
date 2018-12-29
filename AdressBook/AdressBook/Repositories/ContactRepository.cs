using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdressBook.Data;
using AdressBook.Dtos;
using AdressBook.Helpers;
using AdressBook.Models;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;

namespace AdressBook.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;
        private static PhoneNumberUtil _phoneUtil;
        public ContactRepository(DataContext context)
        {
            _context = context;
            _phoneUtil = PhoneNumberUtil.GetInstance();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<bool> ContactIdExist(int Id)
        {
            if (await _context.Contacts.AnyAsync(x => x.Id == Id))
                return true;
            return false;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Contact> GetContact(int contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);

            return contact;
        }

        public async Task<PagedList<Contact>> GetContacts(ContactParams contactParams)
        {
            var contacts = _context.Contacts.OrderByDescending(u => u.Id).AsQueryable();

            if(!string.IsNullOrEmpty(contactParams.SearchText))
            {
                contacts = contacts.Where(

               x => (EF.Functions.Like(x.FirstName, "%"+ contactParams.SearchText + "%")
                   || EF.Functions.Like(x.LastName, "%" + contactParams.SearchText + "%")
                   || EF.Functions.Like(x.Address, "%" + contactParams.SearchText + "%")
                   || EF.Functions.Like(x.TelephoneNumber, "%" + contactParams.SearchText + "%")
                   )

               );
            }
            return await PagedList<Contact>.CreateAsync(contacts, contactParams.PageNumber, contactParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> TelephoneNumberExists(string telephoneNumber)
        {
            if (await _context.Contacts.AnyAsync(x => x.TelephoneNumber == telephoneNumber))
                    return true;
            return false;
        }


    }
}
