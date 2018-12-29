using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdressBook.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using PhoneNumbers;

namespace AdressBook.Helpers
{
    public class PhoneValidateAttribute : ValidationAttribute
    {
        private static PhoneNumberUtil _phoneUtil;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult("Please, you must insert valid phone number"); }

            // if you have problem with DataRead on Windows
            // replace slash with backslash in path!

            var countriesData = System.IO.File.ReadAllText("./Data/Countries.json");
            var countries = JsonConvert.DeserializeObject<List<Countries>>(countriesData);

            var countryCode = ""; // inicijalno

            _phoneUtil = PhoneNumberUtil.GetInstance();
            NumberParseException exception = null;
            try
            {
                // pokušamo parsirati tel. broj pomoću libphonenumber-csharp
                PhoneNumber telephoneNumber = _phoneUtil.Parse(value.ToString(),
                null);

                // Globalni test da li je broj telefona ispravan
                var validNumber = _phoneUtil.IsValidNumber(telephoneNumber);
                if (!validNumber)
                {
                    return new ValidationResult("Please, you must insert valid phone number. This is not valid phone number");
                }

                // iz telefonskog broja parsiramo samo pozivni broj zemlje
                var codePrefix = "+" + telephoneNumber.CountryCode.ToString();

                // u petlji treba naći validan countryCode - primjer "HR" za Hrvatski pozivni broj "+385"
                foreach (var country in countries)
                {
                    if(country.DialCode.Equals(codePrefix))
                    {
                        countryCode = country.Code;
                    }

                }
                // ako nije pronađen validan country code nemoj dozvoliti upis u bazu
                if(countryCode == "")
                {
                    return new ValidationResult("Please, you must insert valid phone number. Country Code is missing or not valid!");
                }

                // provjeri da li je uneseni broj validan za traženu regiju (zemlju)

                bool validNumberForRegion = _phoneUtil.IsValidNumberForRegion(telephoneNumber, countryCode);
                if(!validNumberForRegion)
                {
                    return new ValidationResult("Please, you must insert valid phone number. This is not valid phone number for this region.");
                }

            }
            catch (NumberParseException ex)
            {
                exception = ex;
            }

            if ( exception == null )
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Something went wrong. Please try again!");
            }

            
        }
    }
}
