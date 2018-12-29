using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdressBook.Dtos;
using AdressBook.Helpers;
using AdressBook.Models;
using PhoneNumbers;

namespace AdressBook.Repositories
{
    public interface IContactRepository
    {
        Task<PagedList<Contact>> GetContacts(ContactParams contactParams);
        Task<Contact> GetContact(int contactId);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<bool> TelephoneNumberExists(string telephoneNumber);
        Task<bool> ContactIdExist(int Id);
    }
}
