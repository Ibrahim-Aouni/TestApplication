using Microsoft.EntityFrameworkCore;
using TestApplication.Data.Connexion;
using TestApplication.Data.Interfaces.IDao;
using TestApplication.Data.Models;

namespace TestApplication.Data.DAO
{
    public class PersonDAO : IPersonDAO
    {
        private readonly AppDbContext _context;
        public PersonDAO(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAllPersons(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;


            var persons = await _context.Persons
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return persons;
        }
      
        public async Task<Person> GetPersonById(int id)
        {
            return await _context.Persons.FindAsync(id);
        }
        public async Task<IEnumerable<Person>> AddPerson(IEnumerable<Person> person)
        {
            await _context.Persons.AddRangeAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }
        public async Task DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);  

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {id} not found.");  
            }

            _context.Persons.Remove(person);  
            await _context.SaveChangesAsync(); 
        }
    }
}
