using AutoMapper;

namespace Contact.API.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile() {


            CreateMap<Entities.Report, Model.ReportDto>().ReverseMap();
        }

    }
}
