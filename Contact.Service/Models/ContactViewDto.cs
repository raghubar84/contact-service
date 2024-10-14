using System.ComponentModel.DataAnnotations;

namespace Contact.Service.Models
{
    public class ContactViewDto : ContactDto
    {
        public int Id { get; set; }        
    }
}
