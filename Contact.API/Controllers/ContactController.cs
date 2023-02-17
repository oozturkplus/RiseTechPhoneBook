using AutoMapper;
using Contact.API.Entities;
using Contact.API.Infrastructure;
using Contact.API.Model;
using Contact.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public ContactController(
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
        public async Task<ActionResult<Person>> GetPerson(
            Guid personId)
        {
            
            var person = await _personRepository
                .GetPersonAsync(personId);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PersonDto>(person));
        }



        //[Route("items")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Created)]
        //public async Task<ActionResult> CreateProductAsync([FromBody] CatalogItem product)
        //{
        //    var item = new CatalogItem
        //    {
        //        CatalogBrandId = product.CatalogBrandId,
        //        CatalogTypeId = product.CatalogTypeId,
        //        Description = product.Description,
        //        Name = product.Name,
        //        PictureFileName = product.PictureFileName,
        //        Price = product.Price
        //    };

        //    _catalogContext.CatalogItems.Add(item);

        //    await _catalogContext.SaveChangesAsync();

        //    return CreatedAtAction(nameof(ItemByIdAsync), new { id = item.Id }, null);
        //}
    }
}
