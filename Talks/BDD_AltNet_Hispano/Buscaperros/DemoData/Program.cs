using System.Collections.Generic;
using Domain.Model;
using Domain.Repositories;

namespace DemoData
{
    class Program
    {
        
        static void Main(string[] args)
        {
            InsertData();
        }

        private static void InsertData()
        {
            var repo = new Repository<Status>();
            
                repo.Create(new Status { Id = 0, Name = "Lost" });
                repo.Create(new Status { Id = 1, Name = "Found" });

            var repog = new Repository<Gender>();
            
                repog.Create(new Gender { Id = 0, Name = "Male" });
                repog.Create(new Gender { Id = 1, Name = "Female" });

            var repoa = new Repository<Animal>();
            
                repoa.Create(new Animal { Id = 0, Name = "Dog",Breeds = getDogBreeds()});
                repoa.Create(new Animal { Id = 1, Name = "Cat", Breeds = getCatBreeds()});
            
        }

        private static IEnumerable<Breed> getCatBreeds()
        {
            return new List<Breed>
                       {
                           new Breed {Name = "Angora"},
                           new Breed {Name = "Persian"}
                       };
        }

        private static IEnumerable<Breed> getDogBreeds()
        {
            return new List<Breed>
                       {
                           new Breed {Name = "Poodle"},
                           new Breed {Name = "Golden"},
                           new Breed {Name = "Labrador"},
                           new Breed {Name = "Boston Terrier"}
                       };
        }
    }
}
