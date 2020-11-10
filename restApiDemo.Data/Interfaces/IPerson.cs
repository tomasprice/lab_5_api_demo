using restApiDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace restApiDemo.Data.Interfaces
{
    public interface IPerson
    {
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void Add(Person person);
        void Update(Person person, int id);
        void Delete(int id);
    }
}
