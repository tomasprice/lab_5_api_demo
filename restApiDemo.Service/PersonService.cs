using restApiDemo.Data.Interfaces;
using restApiDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace restApiDemo.Service
{
    public class PersonService : IPerson
    {
        public static List<Person> People = new List<Person>();
        public static int PersonId = 1;

        public void Add(Person person)
        {
            person.Id = PersonId++;
            People.Add(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return People;
        }

        public Person GetById(int id)
        {
            return People
                .Where(person => person.Id == id)
                .FirstOrDefault();
        }

        public void Update(Person person, int id)
        {
            var personIndex = People.FindIndex(person => person.Id == id);

            if (personIndex < 0)
            {
                return;
            }

            var currentPerson = People.Where(person => person.Id == id).FirstOrDefault();
            person.Id = currentPerson.Id;

            People[personIndex] = person;
        }

        public void Delete(int id)
        {
            var personIndex = People.FindIndex(person => person.Id == id);

            if (personIndex < 0)
            {
                return;
            }

            People.RemoveAt(personIndex);
        }
    }
}
