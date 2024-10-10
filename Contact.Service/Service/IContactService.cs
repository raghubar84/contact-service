using Contact.Service.Models;
using Contact.Service.Repository.Entity;

namespace Contact.Service.Service
{
    public interface IContactService
    {
        Task<IEnumerable<ContactViewDto>> GetAll();
        Task<ContactViewDto> GetById(int id);
        Task Add(ContactDto contact);
        Task Update(int id, ContactDto contact);
        Task Delete(int id);
    }
}
