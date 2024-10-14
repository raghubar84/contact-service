using Contact.Service.Controllers;
using Contact.Service.Models;
using Contact.Service.Service;
using Contact.Service.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Contact.Service.Test.Controllers
{
    public class ContactApiControllerTest
    {
        private readonly Mock<IContactService> _contactService;
        private readonly Mock<ILogger<ContactApiController>> _logger;
        private readonly ContactApiController _controller;
        public ContactApiControllerTest()
        {
            _contactService = new Mock<IContactService>();
            _logger = new Mock<ILogger<ContactApiController>>();
            _controller = new ContactApiController(_contactService.Object, _logger.Object);
        }

        [Fact]
        public async void Get_Should_Return_Data()
        {
            _contactService.Setup(x => x.GetAll()).ReturnsAsync(MockData.Contacts);

            var result = await _controller.Get();
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var data = okResult.Value as List<ContactViewDto>;
            Assert.Equal(2, data.Count);
        }

        [Fact]
        public async void GetById_Should_Return_Data()
        {
            _contactService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(MockData.Contacts.First());

            var result = await _controller.Get(1);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var data = okResult.Value as ContactViewDto;
            Assert.Equal("Raghubar", data.FirstName);
        }

        [Fact]
        public async void GetById_Should_Return_404_For_Invalid_Id()
        {
            _contactService.Setup(x => x.GetById(It.IsAny<int>()));

            var result = await _controller.Get(1);
            Assert.IsType<ObjectResult>(result);

            var problemResult = result as ObjectResult;
            Assert.NotNull(problemResult);

            var problem = problemResult.Value as ProblemDetails;
            Assert.Equal(404, problem.Status);
        }

        [Fact]
        public async void Post_Should_Add_Data_For_Valid_Payload()
        {
            var paylod = new ContactDto { FirstName = "Test", LastName = "Test", Email = "test@gmail.com" };
            _contactService.Setup(x => x.Add(It.IsAny<ContactDto>()));

            var result = await _controller.Post(paylod);
            Assert.IsType<StatusCodeResult>(result);

            var statusCodeResult = result as StatusCodeResult;
            Assert.NotNull(statusCodeResult);           
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Post_Should_Return_500_For_Exception()
        {
            var paylod = new ContactDto();
            _contactService.Setup(x => x.Add(It.IsAny<ContactDto>())).ThrowsAsync(new ArgumentException("Exception occured."));          

            var result = await _controller.Post(paylod);
            Assert.IsType<ObjectResult>(result);

            var problemResult = result as ObjectResult;
            Assert.NotNull(problemResult);

            var problem = problemResult.Value as ProblemDetails;
            Assert.Equal(500, problem.Status);
            Assert.Equal("Unexpected exception occured.", problem.Detail);
        }
    }
}
