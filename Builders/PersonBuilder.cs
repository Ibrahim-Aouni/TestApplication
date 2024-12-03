using TestApplication.Data.Models;

namespace TestApplication.Builders
{
    public class PersonBuilder
    {
        private readonly Person _person;
        public PersonBuilder()
        {
            _person = new Person();
        }
        public PersonBuilder(Person person)
        {
            _person = person;
        }
        public PersonBuilder SetFirstName(string firstName)
        { 
            _person.FirstName = firstName;
            return this;
        }
        public PersonBuilder SetLastName(string lastName)
        {
            _person.LastName = lastName;
            return this;
        }
        public PersonBuilder SetAge(int age)
        {
            _person.Age = age;
            return this;
        }
        public Person Build()
        {
            return _person;
        }
    }
}
