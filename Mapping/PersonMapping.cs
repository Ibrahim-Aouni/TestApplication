using TestApplication.Data.DTO;
using TestApplication.Data.Models;

namespace TestApplication.Mapping
{
    public static class PersonMapping
    {
        public static PersonDTO ToDTO(Person person)
        {
            if (person == null) return null;
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age
            };
        }
        public static Person ToModel(PersonDTO personDTO)
        {
            if (personDTO == null) return null;
            return new Person
            {
                Id = personDTO.Id,
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName,
                Age = personDTO.Age
            };
        }
        public static IEnumerable<PersonDTO> ToDTOList(IEnumerable<Person> person)
        {
            return person?.Select(model => ToDTO(model)).ToList() ?? new List<PersonDTO>();
        }
        public static IEnumerable<Person> ToModelList(IEnumerable<PersonDTO> personDTO)
        {
            return personDTO?.Select(dto => ToModel(dto)).ToList()?? new List<Person>();

        }
    }
}
