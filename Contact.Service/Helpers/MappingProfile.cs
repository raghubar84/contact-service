using AutoMapper;
using Contact.Service.Models;
using Contact.Service.Repository.Entity;

namespace Contact.Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Contacts, ContactDto>();
            CreateMap<Contacts, ContactViewDto>();
            CreateMap<ContactDto, Contacts>();
        }
    }
}
