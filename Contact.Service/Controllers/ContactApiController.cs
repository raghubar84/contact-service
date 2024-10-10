using Contact.Service.Models;
using Contact.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactApiController> _logger;
        public ContactApiController(IContactService contactService, ILogger<ContactApiController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        // GET: api/<ContactApiController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _contactService.GetAll();
            return Ok(contacts);
        }

        // GET api/<ContactApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _contactService.GetById(id);
            if(contact is null)
            {
                _logger.LogInformation($"{nameof(ContactApiController)}: Contact with id {id} not found.");
                return Problem(statusCode:404,detail: $"Contact with id {id} not found.");
            }

            return Ok(contact);
        }

        // POST api/<ContactApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto contactDto)
        {
            try
            {               
                await _contactService.Add(contactDto);
                _logger.LogInformation($"{nameof(ContactApiController)}: Contact added successfuly with name {contactDto.FirstName}.");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ContactApiController)}: Exception occured while adding contact with name {contactDto.FirstName}.");
                return Problem(statusCode: 500, detail: "Unexpected exception occured.");
            }
        }

        // PUT api/<ContactApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactDto contactDto)
        {
            try
            {
                await _contactService.Update(id, contactDto);
                _logger.LogInformation($"{nameof(ContactApiController)}: Contact updated successfuly with id {id}.");
                return Ok();
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex, $"{nameof(ContactApiController)}: Exception occured while updating contact with id {id}.");
                return Problem(statusCode: 400, detail: ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ContactApiController)}: Exception occured while updating contact with id {id}.");
                return Problem(statusCode: 500, detail: "Unexpected exception occured.");
            }
        }

        // DELETE api/<ContactApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _contactService.Delete(id);
                _logger.LogInformation($"{nameof(ContactApiController)}: Contact deleted successfuly with id {id}.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ContactApiController)}: Exception occured while deleting contact with id {id}.");
                return Problem(statusCode: 500, detail: "Unexpected exception occured.");
            }
        }
    }
}
