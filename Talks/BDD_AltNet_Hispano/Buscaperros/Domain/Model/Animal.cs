using System.Collections.Generic;

namespace Domain.Model
{
    public class Animal 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Breed> Breeds { get; set; }
    }
}