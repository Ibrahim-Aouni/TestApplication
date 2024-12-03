using TestApplication.Data.DTO;
using TestApplication.Data.Models;

namespace TestApplication.Mapping
{
    public static class PersonSup40Mapping
    {
        public static PersonDTO ToDTO(PersonSup40 person)
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
        public static PersonSup40 ToModel(PersonDTO personDTO)
        {
            if (personDTO == null) return null;
            return new PersonSup40
            {
                Id = personDTO.Id,
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName,
                Age = personDTO.Age
            };
        }
        public static IEnumerable<PersonDTO> ToDTOList(IEnumerable<PersonSup40> person)
        {
            return person?.Select(model => ToDTO(model)).ToList() ?? new List<PersonDTO>();
        }
        public static IEnumerable<PersonSup40> ToModelList(IEnumerable<PersonDTO> personDTO)
        {
            return personDTO?.Select(dto => ToModel(dto)).ToList() ?? new List<PersonSup40>();

        }
    }
}
