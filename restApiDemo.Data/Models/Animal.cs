using System;
using System.Collections.Generic;
using System.Text;

namespace restApiDemo.Data.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Weight { get; set; }
        public bool Carnivorous { get; set; }
        public List<DateTime> CaptureDates { get; set; }
        public List<string> PlacesOfOccurrence { get; set; }
    }
}
