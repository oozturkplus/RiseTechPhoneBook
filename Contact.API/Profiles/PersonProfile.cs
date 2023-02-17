using AutoMapper;

namespace Contact.API.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Entities.Person, Model.PersonDto>();
            CreateMap<Model.PersonDto, Entities.Person>();
            CreateMap<Model.PersonForCreationDto, Entities.Person>();
        }
    }
}
