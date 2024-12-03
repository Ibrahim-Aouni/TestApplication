using TestApplication.Data.Connexion;
using TestApplication.Data.Interfaces.IDao;
using TestApplication.Data.Models;

namespace TestApplication.Data.DAO
{
    public class PersonDAOSup40 : IPersonDAOSup40
    {
        private readonly AppDbContext _context;
        public PersonDAOSup40(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PersonSup40>> AddPerson40(IEnumerable<PersonSup40> person)
        {
            await _context.PersonsSup40s.AddRangeAsync(person);
            await _context.SaveChangesAsync();
            return _context.PersonsSup40s.ToList();

        }
    }
}
