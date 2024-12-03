using TestApplication.Data.Models;

namespace TestApplication.Data.Interfaces.IDao
{
    public interface IPersonDAOSup40
    {
        Task<IEnumerable<PersonSup40>> AddPerson40(IEnumerable<PersonSup40> person);
    }
}
