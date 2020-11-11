using restApiDemo.Data.Interfaces;
using restApiDemo.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace restApiDemo.Service
{
    public class AnimalService : IAnimal
    {
        public static List<Animal> Animals = new List<Animal>();
        public static int AnimalId = 1;

        public void Add(Animal animal)
        {
            animal.Id = AnimalId++;
            Animals.Add(animal);
        }

        public void Delete(int id)
        {
            var animalIndex = Animals
                .FindIndex(animal => animal.Id == id);

            if (animalIndex < 0)
            {
                return;
            }

            Animals.RemoveAt(animalIndex);
        }

        public IEnumerable<Animal> GetAll()
        {
            return Animals;
        }

        public Animal GetById(int id)
        {
            return Animals
                .Where(animal => animal.Id == id)
                .FirstOrDefault();
        }

        public void Update(Animal animal, int id)
        {
            var animalIndex = Animals
                .FindIndex(animal => animal.Id == id);

            var currentAnimal = Animals
                .Where(animal => animal.Id == id)
                .FirstOrDefault();

            if (animalIndex < 0)
            {
                return;
            }

            animal.Id = currentAnimal.Id;
            Animals[animalIndex] = animal;
        }
    }
}
