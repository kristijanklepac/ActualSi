using System;
namespace AdressBook.Dtos
{
    public class ContactForListDto
    {
        public ContactForListDto()
        {
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
