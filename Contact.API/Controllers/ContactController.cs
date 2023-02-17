using Contact.API.Model;
using Microsoft.AspNetCore.Http;
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
    public class ContactController : ControllerBase
    {


        [Route("contacts")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<PersonDto>> CreateContactAsync(
            PersonForCreationDto personForCreationDto)
        {

            return Ok(1);
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
