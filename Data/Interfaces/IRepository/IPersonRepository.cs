using Microsoft.AspNetCore.Mvc;
using TestApplication.Data.DTO;

namespace TestApplication.Data.Interfaces.IRepository
{
    public interface IPersonRepository
    {
        Task<IEnumerable<PersonDTO>> GetAllPersonDTO(int page, int pageSize);
        Task<IEnumerable<PersonDTO>> SlipDataPersons(IEnumerable<PersonDTO> personDTO);
        Task<PersonDTO> GetPersonDTOById(int id);
        Task<IActionResult> AddPersonDTO(IEnumerable<PersonDTO> personDTO);
        Task<IEnumerable<PersonDTO>> CreatePersonSup40(IEnumerable<PersonDTO> personDTO);
        Task DeletePerson(int id);
    }
}
