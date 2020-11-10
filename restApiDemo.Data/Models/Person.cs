using System;

namespace restApiDemo.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthYear { get; set; }
        public string Town { get; set; }
    }
}
