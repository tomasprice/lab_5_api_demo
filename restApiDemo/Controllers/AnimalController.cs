using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using restApiDemo.Data.Interfaces;
using restApiDemo.Data.Models;

namespace restApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimal _animalService;

        public AnimalController(IAnimal animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public IEnumerable<Animal> Get()
        {
            return _animalService.GetAll();
        }

        [HttpGet("{id}")]
        public Animal Get(int id)
        {
            return _animalService.GetById(id);
        }

        [HttpPost]
        public void Add(Animal model)
        {
            _animalService.Add(model);
        }

        [HttpPut("{id}")]
        public void Update(Animal model, int id)
        {
            _animalService.Update(model, id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _animalService.Delete(id);
        }
    }
}
