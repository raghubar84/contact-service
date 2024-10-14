using Contact.Service.Repository.Entity;
using System.Text.Json;
using System;

namespace Contact.Service.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly List<Contacts> contacts;
        private string jsonPath = string.Empty;
        public ContactRepository(IHostEnvironment hostEnvironment)
        {
            jsonPath = hostEnvironment.ContentRootPath + @"\db.json";
            using StreamReader r = new StreamReader(jsonPath);
            string json = r.ReadToEnd();
            contacts = JsonSerializer.Deserialize<List<Contacts>>(json)!;
        }
        public async Task Add(Contacts contact)
        {
            contact.Id = contacts.Max(x => x.Id) + 1;
            contacts.Add(contact);
            await this.SaveChanges();            
        }

        public async Task Delete(int id)
        {
            var index = contacts.FindIndex(x => x.Id == id);
            contacts.RemoveAt(index);
            await this.SaveChanges();
        }

        public async Task<IEnumerable<Contacts>> GetAll() => await Task.FromResult(contacts.ToList());

        public async Task<Contacts?> GetById(int id)
        {
            var contact = contacts.Find(x => x.Id == id);
            return await Task.FromResult(contact);
        }

        public async Task Update(Contacts contact)
        {
            var index = contacts.FindIndex(x => x.Id == contact.Id);
            contacts.RemoveAt(index);
            contacts.Add(contact);
            await this.SaveChanges();
        }

        public Task SaveChanges()
        {
            //Sort data before saving to json file
            var data = contacts.OrderBy(x => x.Id).ToList();
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter(jsonPath))
            {
                outputFile.WriteLine(jsonString);
            }

            return Task.CompletedTask;
        }
    }
}
