using System.ComponentModel.DataAnnotations;

namespace Contact.Service.Repository.Entity
{
    //Contact name is conflicting with namespace
    public class Contacts
    {
        public int Id { get; set; }        
        public required string FirstName { get; set; }        
        public required string LastName { get; set; }        
        public required string Email { get; set; }        
    }
}
