using System;
using System.Collections.Generic;
using AdressBook.Models;
using Newtonsoft.Json;

namespace AdressBook.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedContacts()
        {

            // if you have problem with DataRead on Windows
            // replace slash with backslash in path!

                var contactData = System.IO.File.ReadAllText("Data/ContactSeedData.json");
                var contacts = JsonConvert.DeserializeObject<List<Contact>>(contactData);

                foreach (var contact in contacts)
                {
                    _context.Contacts.Add(contact);
                }

                _context.SaveChanges();

            }

    }

   
}
