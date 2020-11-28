using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restApiDemo.Data.Interfaces;
using restApiDemo.Data.Models;

namespace restApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase  
    {
        private readonly IPerson _personService;
        //2019-01-06T17:16:40

        public PersonController(IPerson personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public void Add(Person model,
            bool townRestrict,
            bool demoData)
        {
            if (demoData)
            {
                var personList = new List<Person>
            {
                { new Person {
                    Name = "Tomasz",
                    Surname = "Test",
                    Town = "Warszawa",
                    BirthYear = Convert.ToDateTime("1990-01-06T17:16:40")}
                },
                { new Person {
                    Name = "tomasz",
                    Surname = "Test",
                    Town = "Łódź",
                    BirthYear = Convert.ToDateTime("2000-01-06T17:16:40")}
                },
                { new Person {
                    Name = "ToMaSz",
                    Surname = "Test",
                    Town = "Gdańsk",
                    BirthYear = Convert.ToDateTime("1998-01-06T17:16:40")}
                },
                { new Person {
                    Name = "MichAł",
                    Surname = "Test",
                    Town = "Siedlce",
                    BirthYear = Convert.ToDateTime("2019-01-06T17:16:40")}
                },
                { new Person {
                    Name = "MICHał",
                    Surname = "Test",
                    Town = "Ełk",
                    BirthYear = Convert.ToDateTime("1998-01-06T17:16:40")}
                },
                { new Person {
                    Name = "Michał",
                    Surname = "Test",
                    Town = "Biała Podlaska",
                    BirthYear = Convert.ToDateTime("1998-01-06T17:16:40")}
                },
                { new Person {
                    Name = "Jan",
                    Surname = "Test",
                    Town = "Gdynia",
                    BirthYear = Convert.ToDateTime("1990-01-06T17:16:40")}
                },
                { new Person {
                    Name = "Ala",
                    Surname = "Test",
                    Town = "Gdynia",
                    BirthYear = Convert.ToDateTime("1990-01-06T17:16:40")}
                },
            };

                foreach (var personDemo in personList)
                {
                    _personService.Add(personDemo);
                }

                return;
            }

            var person = GetPersonModel(model);

            if (townRestrict)
            {
                var townAdded = GetPeopleFromTownCount(person.Town);

                if (townAdded == 0)
                {
                    return;
                }
            }
            _personService.Add(person);           
        }

        [HttpGet]
        public IEnumerable<Person> Get(
            string name,
            int birthYear,
            string town,
            bool matchCase,
            bool letterCaseSensitivity)
        {
            var tempList = _personService.GetAll().ToList();

            if (!string.IsNullOrEmpty(name))
            {
                tempList = GetByName(tempList, name, matchCase, letterCaseSensitivity).ToList();
            }

            if (birthYear.ToString().Length >= 2 && birthYear.ToString().Length <= 4 && birthYear != -1)
            {
                tempList = GetByBirthYear(tempList, birthYear).ToList();
            }

            if (!string.IsNullOrEmpty(town))
            {
                tempList = GetByTown(tempList, town, matchCase, letterCaseSensitivity).ToList();
            }

            return tempList;
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _personService.GetById(id);
        }

        [HttpPut("{id}")]
        public void Update(Person model, int id)
        {
            var check = _personService.GetAll().Count();

            if (id > check)
            {
                return;
            }

            var person = GetPersonModel(model);

            _personService.Update(person, id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _personService.Delete(id);
        }


        // Custom filtering
        private IEnumerable<Person> GetByName(List<Person> personList, string name,
            bool matchCase,
            bool letterCaseSensitivity)
        {
            if (matchCase)
            {
                if (letterCaseSensitivity)
                {
                    return personList
                        .Where(person => person.Name == name);
                }

                return personList
                    .Where(person => person.Name.ToLower() == name.ToLower());
            }

            if (letterCaseSensitivity)
            {
                return personList
                   .Where(person => person.Name.Contains(name));
            }

            return personList
                .Where(person => person.Name.ToLower().Contains(name.ToLower()));
        }

        private IEnumerable<Person> GetByBirthYear(List<Person> personList, int query)
        {
            return personList
                .Where(person => person.BirthYear.Year == query);
        }

        private IEnumerable<Person> GetByTown(List<Person> personList, string town,
          bool matchCase,
          bool letterCaseSensitivity)
        {
            if (matchCase)
            {
                if (letterCaseSensitivity)
                {
                    return personList
                        .Where(person => person.Town == town);
                }

                return personList
                    .Where(person => person.Town.ToLower() == town.ToLower());
            }

            if (letterCaseSensitivity)
            {
                return personList
                   .Where(person => person.Town.Contains(town));
            }

            return personList
                .Where(person => person.Town.ToLower().Contains(town.ToLower()));
        }

        private static Person GetPersonModel(Person model)
        {
            return new Person
            {
                BirthYear = Convert.ToDateTime(model.BirthYear),
                Surname = model.Surname,
                Name = model.Name,
                Town = model.Town
            };

        }

        private int GetPeopleFromTownCount(string town)
        {
            return _personService.GetAll()
                .Where(person => person.Town == town)
                .Count();
        }
    }
}
