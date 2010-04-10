using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicProg.Data;
using DynamicProg.Extensions;

namespace DataCreator
{
    class Program
    {
        private static SqlDb _db;

        static Program()
        {
            _db = new SqlDb();
        }

        static void Main(string[] args)
        {
            DeleteAllData();
            AddAnimals();
            AddStatus();
            AddGenders();
            AddPetsForUser("a3b748fa-cfe5-4e86-9341-0932ff11911d");
            //DeletePetsForUser("a3b748fa-cfe5-4e86-9341-0932ff11911d");
        }

        private static void AddPetsForUser(string a3b748faCfe5E86Ff11911d)
        {
            
        }

        private static void AddGenders()
        {
            foreach (var gender in new[] { "Male", "Female" })
            {
                _db.Query("Insert into Gender (Name) values (@gender)")
                    .AddParameter("gender", gender)
                    .NonQuery();
            }
        }

        private static void AddStatus()
        {
            foreach (var status in new[]{"Lost","Found"})
            {
                _db.Query("Insert into Status (Name) values (@status)")
                    .AddParameter("status", status)
                    .NonQuery();
            }
        }

        private static void AddAnimals()
        {
            AddDogs();
            AddCats();
        }

        private static void AddCats()
        {
            _db.Query("Insert into Animal (Name) values (@animal)")
                .AddParameter("animal", "Cat")
                .NonQuery();

            var id = _db.Query("SELECT id FROM Animal WHERE Name = @animal")
                .AddParameter("animal", "Cat")
                .GetUniqueResult<int>();

            AddBreeds(new[]{"Siamese","Angora","Mix"},id);
        }

        private static void AddDogs()
        {
            _db.Query("Insert into Animal (Name) values (@animal)")
                .AddParameter("animal", "Dog")
                .NonQuery();

            var id = _db.Query("SELECT id FROM Animal WHERE Name = @animal")
                .AddParameter("animal", "Dog")
                .GetUniqueResult<int>();

            AddBreeds(new[] {"Doodle","Dalmata","Terrier","Bulldog","Great dane","Mix" }, id);
        }

        private static void AddBreeds(IEnumerable<string> breeds, int animalId)
        {
            foreach (var breed in breeds)
            {
                _db.Query("Insert Into Breed (Name, AnimalId) values (@breed, @animalId)")
                    .AddParameter("breed", breed)
                    .AddParameter("animalId",animalId)
                    .NonQuery();
            }
        }

        private static void DeletePetsForUser(string userId)
        {
            _db.Query(string.Format("delete from Pet where UserId = {0}", userId))
                .NonQuery();
        }

        private static void DeleteAllData()
        {
            var tables = new[] {"Pet", "Gender", "Status", "Breed", "Animal"};
            foreach (var table in tables)
            {
                _db.Query(string.Format("delete from {0}", table))
                    .NonQuery();
            }
        }
    }
}
