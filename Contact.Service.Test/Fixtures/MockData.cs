using Contact.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Service.Test.Fixtures
{
    internal static class MockData
    {
        public static List<ContactViewDto> Contacts =>
        [
            new ContactViewDto { Id = 1, FirstName = "Raghubar", LastName = "Gupta", Email = "raghubar@gmail.com" },
            new ContactViewDto { Id = 2, FirstName = "Deb", LastName = "Rathi", Email = "deb@gmail.com" },
        ];
    }
}
