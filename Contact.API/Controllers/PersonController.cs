using AutoMapper;
using Contact.API.Entities;
using Contact.API.Model;
using Contact.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public PersonController(
            IMapper mapper, IPersonRepository  personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }


        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PersonDto>> CreatePersonAsync(
            PersonForCreationDto personForCreationDto)
        {

            Person finalPerson=_mapper.Map<Person>(personForCreationDto);

            await _personRepository.AddPersonAsync(finalPerson);

            await _personRepository.SaveChangesAsync();


            var createdPersonForReturn =
                _mapper.Map<Model.PersonDto>(finalPerson);

            return CreatedAtRoute("GetPerson",
                 new
                 {
                     personId = finalPerson.Id
                 },
                 createdPersonForReturn);
        }

        [HttpGet("{personId}", Name = "GetPerson")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Person>> GetPersonAsync(
            Guid personId)
        {
            
            var person = await _personRepository
                .GetPersonAsync(personId,true);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PersonDto>(person));
        }





        [HttpDelete("{personId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePerson(
            Guid personId)
        {

            var personEntity = await _personRepository
                .GetPersonAsync(personId,false);
           
            if (personEntity == null)
            {
                return NotFound();
            }

            _personRepository.RemovePersonAsync(personEntity);

            await _personRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("persons")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<PersonDto>> GetAllPersonAsync()
        {
            var persons = await _personRepository.GetPersonsAsync(false);

            var personsDto = persons.Select(
                prs => _mapper.Map<PersonDto>(prs)
                );

            return personsDto;
        }
    }
}
