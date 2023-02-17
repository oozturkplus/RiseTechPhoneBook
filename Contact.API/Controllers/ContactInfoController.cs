using AutoMapper;
using Contact.API.Entities;
using Contact.API.Model;
using Contact.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Contact.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public ContactInfoController(
            IMapper mapper, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ContactInfoDto>> AddContactInfoAsync(
            Guid personId,
            ContactInfoForCreationDto contactInfoForCreationDto)
        {
            if (!await _personRepository.PersonExistsAsync(personId))
            {
                return NotFound();
            }


            ContactInfo finalContactInfo = _mapper.Map<ContactInfo>(contactInfoForCreationDto);

            await _personRepository.AddContactInfoAsync(personId,
                finalContactInfo);

            await _personRepository.SaveChangesAsync();

            Person updatedPerson=await _personRepository.GetPersonAsync(personId,false);
            var updatedPersonDto =
                _mapper.Map<Model.PersonDto>(updatedPerson);

            return CreatedAtRoute("GetPerson",
                 new
                 {
                     personId = updatedPerson.Id
                 },
                 updatedPersonDto);
        }
    }
}
