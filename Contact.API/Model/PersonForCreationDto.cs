using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.Model
{
    public class PersonForCreationDto
    {
        [Required(ErrorMessage = "You should provide first name")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide last name")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
    }
}
