using Contact.API.Entities;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Contact.API.Model
{
    public class ContactInfoForCreationDto:IValidatableObject
    {
        [EnumDataType(typeof(ContactInfoType))]
        [DefaultValue(1)]
        [Required(ErrorMessage = "You should contact info type")]
        public int ContactInfoTypeId { get; set; }

        [Required(ErrorMessage = "You should provide info")]
        public string Info { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(ContactInfoTypeId.ToString(), true, out ContactInfoType result))
            {
                yield return new ValidationResult("Invalid contact info type", new[] { nameof(ContactInfoType) });
            }

            ContactInfoTypeId = (int)result; //normalize Type
        }

    }
}
