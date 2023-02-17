using AutoMapper;
using Contact.API.Entities;
using System;

namespace Contact.API.Profiles
{
    public class ContactInfoProfile : Profile
    {
        public ContactInfoProfile()
        {
            CreateMap<Entities.ContactInfo, Model.ContactInfoDto>().ReverseMap();

            CreateMap<Entities.ContactInfo, Model.ContactInfoDto>()
                .ForMember(d=>d.ContactInfoType,
                            opt=>opt.MapFrom(source=>Enum.GetName(typeof(ContactInfoType),
                            source.ContactInfoTypeId))).ReverseMap();

            //CreateMap<Model.ContactInfoDto, Entities.ContactInfo>()
            //    .ConvertUsingEnumMapping(opt => opt
            //    // optional: .MapByValue() or MapByName(), without configuration MapByValue is used
            //    .MapValue(Source.First, Destination.Default));

            CreateMap<Model.ContactInfoForCreationDto, Entities.ContactInfo>().ReverseMap();
        }
    }
}
