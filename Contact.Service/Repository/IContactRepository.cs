using Contact.Service.Repository.Entity;

namespace Contact.Service.Repository
{
    public interface IContactRepository
    {
        Task<Contacts?> GetById(int id);
        Task<IEnumerable<Contacts>> GetAll();
        Task Add(Contacts contact);
        Task Update(Contacts contact);
        Task Delete(int id);
    }
}
