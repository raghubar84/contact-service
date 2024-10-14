using AutoMapper;
using Contact.Service.Models;
using Contact.Service.Repository;
using Contact.Service.Repository.Entity;

namespace Contact.Service.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task Add(ContactDto contact)
        {   
            var data = _mapper.Map<Contacts>(contact);
            await _contactRepository.Add(data);
        }

        public async Task Delete(int id)
        {
            var data = await _contactRepository.GetById(id);
            if (data is null)
            {
                throw new ArgumentException($"Contact with id {id} not found.");
            }

            await _contactRepository.Delete(id);
        }

        public async Task<IEnumerable<ContactViewDto>> GetAll()
        {
            var data = await _contactRepository.GetAll();
            var contacts = _mapper.Map<List<ContactViewDto>>(data);
            return contacts;
        }

        public async Task<ContactViewDto> GetById(int id)
        {
            var data = await _contactRepository.GetById(id);
            var contact = _mapper.Map<ContactViewDto>(data);
            return contact;
        }

        public async Task Update(int id, ContactDto contact)
        {  
            var data = await _contactRepository.GetById(id);
            if(data is null)
            {
                throw new ArgumentException($"Contact with id {id} not found.");
            }

            var updatedContact = _mapper.Map<Contacts>(contact);
            updatedContact.Id = id;
            await _contactRepository.Update(updatedContact);
        }
    }
}
