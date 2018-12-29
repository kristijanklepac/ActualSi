using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdressBook.Data;
using AdressBook.Dtos;
using AdressBook.Helpers;
using AdressBook.Models;
using AdressBook.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;

namespace AdressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;


        public AddressController(IContactRepository contactRepository, IMapper mapper )
        {
            _mapper = mapper;
            _contactRepository = contactRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery]ContactParams contactParams)
        {
            var contacts = await _contactRepository.GetContacts(contactParams);

            var contactsToReturn = _mapper.Map<IEnumerable<ContactForListDto>>(contacts);

            Response.AddPagination(contacts.CurrentPage, contacts.PageSize,
                contacts.TotalCount, contacts.TotalPages);

            return Ok(contactsToReturn);

        }

        [HttpGet("{id}", Name = "GetContact")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _contactRepository.GetContact(id);

            if(contact == null)
            {
                return BadRequest("Unknown contact");
            }

            var contactToReturn = _mapper.Map<ContactForListDto>(contact);

            return Ok(contactToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactForAddDto contactForAddDto)
        {
            var contact = _mapper.Map<Contact>(contactForAddDto);

            // check if phone number already exist
            var phoneExists = await _contactRepository.TelephoneNumberExists(contactForAddDto.TelephoneNumber);

            if (phoneExists)
            {
                return BadRequest("Phone already exist");
            }

            _contactRepository.Add(contact);

            if (await _contactRepository.SaveAll())
            {
                var contactToReturn = _mapper.Map<ContactForListDto>(contact);
                return CreatedAtRoute("GetContact", new { id = contact.Id }, contactToReturn);
            }

            throw new Exception("Creating the contact failed on save");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactForUpdateDto contactForUpdateDto)
        {
            if (!await _contactRepository.ContactIdExist(id))
                return BadRequest("Unknown Contact");

            var contactFromRepo = await _contactRepository.GetContact(id);

            _mapper.Map(contactForUpdateDto, contactFromRepo);

            if (await _contactRepository.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (!await _contactRepository.ContactIdExist(id))
                return BadRequest("Unknown Contact");

            var contactFromRepo = await _contactRepository.GetContact(id);

            _contactRepository.Delete(contactFromRepo);

            if (await _contactRepository.SaveAll())
                return NoContent();

            throw new Exception("Error deleting the message");
        }
    }
}
