using AutoMapper;

namespace Contact.API.Profiles
{
    public class ContactInfoProfile : Profile
    {
        public ContactInfoProfile()
        {
            CreateMap<Entities.ContactInfo, Model.ContactInfoDto>();
            CreateMap<Model.ContactInfoDto,Entities.ContactInfo>();
            CreateMap<Model.ContactInfoForCreationDto, Entities.ContactInfo>();
            CreateMap<Entities.ContactInfo,Model.ContactInfoForCreationDto>();
        }
    }
}
