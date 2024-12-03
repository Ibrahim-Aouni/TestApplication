using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TestApplication.Data.Connexion;
using TestApplication.Data.DTO;
using TestApplication.Data.Interfaces.IRepository;
using TestApplication.Data.Interfaces.IService;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController: ControllerBase
    {
        public readonly IPersonRepository _personRepository;
        public readonly IFileLogger _fileLogger;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonRepository personRepository, IFileLogger fileLogger, ILogger<PersonController> logger)
        {
           _personRepository = personRepository;
            _fileLogger = fileLogger;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetPersons(int page = 1, int pageSize = 10)
        {
            try
            {
                var persons = await _personRepository.GetAllPersonDTO(page, pageSize);
                return Ok(persons);
            }catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching all persons: {ex.Message}");
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost("api/Person/AddBDDAndTxt")]
        public async Task<IActionResult> AddBDDAndTxt(IEnumerable<PersonDTO> personDTO)
        {
            if (personDTO == null || !personDTO.Any())
            {
                return BadRequest(new { message = "The provided data is null or empty." });
            }

            try
            {
                var personsOver40 = personDTO.Where(p => p.Age > 40).ToList();
                var personsUnder40 = personDTO.Where(p => p.Age <= 40).ToList();

                switch (personsOver40.Any())
                {
                    case true:
                        await _personRepository.CreatePersonSup40(personsUnder40);
                        _logger.LogInformation($"Added {personsOver40.Count} person(s) over 40 years old.");

                        var persons = await _personRepository.SlipDataPersons(personsOver40);
                        return Ok(persons);

                    case false:
                        if (personsUnder40.Any())
                        {
                            var person = await _personRepository.AddPersonDTO(personsOver40);
                            return Ok(person);
                        }
                        return BadRequest(new { message = "No valid persons to process." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing the persons data: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }


        [HttpPost("api/Person/AddPerson")]
        public async Task<IActionResult> AddPerson(IEnumerable<PersonDTO> personDTO)
        {
            try
            {
                var newPerson = await _personRepository.AddPersonDTO(personDTO);
                if (newPerson == null)
                {
                    return BadRequest("Unable to add the person(s).");
                }
                return newPerson;
            }catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding person(s): {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
            
        }
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeletePerson(int id)
        {
            await _personRepository.DeletePerson(id);
            return NoContent();
        }
    }
}
