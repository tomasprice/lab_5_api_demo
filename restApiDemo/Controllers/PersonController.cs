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
        public void Add(Person model)
        {
            var person = GetPersonModel(model);

            _personService.Add(person);
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAll();
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
    }
}
