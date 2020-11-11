using restApiDemo.Data.Models;
using System.Collections.Generic;

namespace restApiDemo.Data.Interfaces
{
    public interface IAnimal
    {
        void Add(Animal animal);
        void Update(Animal animal, int id);
        void Delete(int id);
        Animal GetById(int id);
        IEnumerable<Animal> GetAll();
    }
}
