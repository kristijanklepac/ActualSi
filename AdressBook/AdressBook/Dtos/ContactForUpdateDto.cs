using System;
using System.ComponentModel.DataAnnotations;
using AdressBook.Helpers;

namespace AdressBook.Dtos
{
    public class ContactForUpdateDto
    {
        public ContactForUpdateDto()
        {
        }
        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "First Name cannot be longer than 40 characters and less than 2 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Last Name cannot be longer than 40 characters and less than 2 characters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Address cannot be longer than 255 characters and less than 5 characters")]
        public string Address { get; set; }
        [Required]
        // Custom validation for phone numbers
        // Author: Kristijan Klepac
        // Look at Helpers/PhoneValidateAttribute.cs
        [PhoneValidate]
        public string TelephoneNumber { get; set; }
    }
}
