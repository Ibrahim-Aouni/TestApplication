using Microsoft.AspNetCore.Mvc;
using TestApplication.Data.DAO;
using TestApplication.Data.DTO;
using TestApplication.Data.Interfaces.IDao;
using TestApplication.Data.Interfaces.IRepository;
using TestApplication.Data.Interfaces.IService;
using TestApplication.Data.Models;
using TestApplication.Mapping;

namespace TestApplication.Data.Repository
{
    public class PersonRepository:ControllerBase, IPersonRepository
    {
        private readonly IPersonDAO _personDAO;
        private readonly IPersonDAOSup40 _personDAOSup40;
        private readonly IFileLogger _fileLogger;

        private readonly string logFilePath = Environment.GetEnvironmentVariable("LOG_FILE_PATH");

        public PersonRepository(IPersonDAO personDAO, IPersonDAOSup40 personDAOSup40, IFileLogger fileLogger)
        {
            _personDAO = personDAO;
            _personDAOSup40 = personDAOSup40;
            _fileLogger = fileLogger ?? throw new ArgumentNullException(nameof(fileLogger));
        }
        public async Task<IEnumerable<PersonDTO>> GetAllPersonDTO(int page, int pageSize)
        {
            var persons = await _personDAO.GetAllPersons(page, pageSize);
            return PersonMapping.ToDTOList(persons);

        }
        public async Task<PersonDTO> GetPersonDTOById(int id)
        {
            var person = await _personDAO.GetPersonById(id);
            return PersonMapping.ToDTO(person);
        }
        public async Task<IEnumerable<PersonDTO>> CreatePersonSup40(IEnumerable<PersonDTO> personDTO)
        {
            var person = PersonSup40Mapping.ToModelList(personDTO);
            var addedPerson = await _personDAOSup40.AddPerson40(person);
            return PersonSup40Mapping.ToDTOList(addedPerson);

        }
        public async Task<IEnumerable<PersonDTO>> CreatePerson(IEnumerable<PersonDTO> personDTO)
        {
            var person = PersonMapping.ToModelList(personDTO);
            var addedPerson = await _personDAO.AddPerson(person);
            return PersonMapping.ToDTOList(addedPerson);
        }
        public async Task<IActionResult> AddPersonDTO(IEnumerable<PersonDTO> personDTO)
        {
            if (personDTO == null || !personDTO.Any())
            {
                return BadRequest("The person list is empty.");
            }

            var tasks = personDTO.GroupBy(p => p.Age >= 40)
                                  .Select(group => group.Key switch
                                  {
                                      true => CreatePersonSup40(group),
                                      false => CreatePerson(group)
                                  });

            var results = await Task.WhenAll(tasks);

            var allAddedPersons = results.SelectMany(r => r);
            return Ok(allAddedPersons);

        }
        public async Task<IEnumerable<PersonDTO>> SlipDataPersons(IEnumerable<PersonDTO> personDTO)
        {
            await AddPersonDTO(personDTO);
            
            _fileLogger.LogMessage(personDTO, logFilePath);

            return personDTO;
        }

        public async Task DeletePerson(int id)
        {
           var person =  await _personDAO.GetPersonById(id);
            if(person == null)
            {
                await _personDAO.DeletePerson(id);
            }
        }
    }
}
