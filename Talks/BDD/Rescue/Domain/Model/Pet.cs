using System;

namespace Domain.Model
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public virtual Breed Breed { get; set; }
        public int Age { get; set; }
        public Status Status { get; set; }
        public string Picture { get; set; }
    }
}