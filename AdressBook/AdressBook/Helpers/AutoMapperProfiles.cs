using System;
using AdressBook.Dtos;
using AdressBook.Models;
using AutoMapper;

namespace AdressBook.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactParams>();
            CreateMap<ContactForAddDto, Contact>();
            CreateMap<ContactForUpdateDto, Contact>();
        }
    }
}
