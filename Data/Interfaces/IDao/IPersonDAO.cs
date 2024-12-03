using TestApplication.Data.Models;

namespace TestApplication.Data.Interfaces.IDao
{
    public interface IPersonDAO
    {
        Task<IEnumerable<Person>> GetAllPersons(int page, int pageSize);
        Task<Person> GetPersonById(int id);
        Task<IEnumerable<Person>> AddPerson(IEnumerable<Person> person);
        Task DeletePerson(int id);
    }
}
