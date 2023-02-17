using System.ComponentModel.DataAnnotations;

namespace Contact.API.Model
{
    public class ContactInfoForCreationDto
    {

        [Required(ErrorMessage = "You should contact info type")]
        public int ContactInfoTypeId { get; set; }

        [Required(ErrorMessage = "You should provide info")]
        public string Info { get; set; }

    }
}
