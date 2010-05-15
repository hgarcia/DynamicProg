using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Model;
using Domain.Repositories;

namespace Web.Controllers
{
    public class PublishController : Controller
    {
        //
        // GET: /Publish/
        public ActionResult Index()
        {
            IEnumerable<Pet> pets = new List<Pet>();
            var repo = new Repository<Pet>();
            pets = repo.GetAll();

            //return View();
            return View(pets);
        }

        //
        // GET: /Publish/Create
        public ActionResult Create()
        {
            var breeds = new Repository<Animal>().GetAll().First(an => an.Name.ToLower() == "dog").Breeds.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Name
            });
            var status = new Repository<Status>().GetAll().Select(t => new SelectListItem
                                                                           {
                                                                               Text = t.Name,
                                                                               Value = t.Name
                                                                           });
            var viewModel = new PetViewModel { Breeds = breeds, Statuses = status };
            return View(viewModel);
            //return View();
        }

        //
        // POST: /Publish/Create
        [HttpPost]
        public ActionResult Create(FormCollection petmodel)
        {
            try
            {
                var repo = new Repository<Pet>();
                var pet = new Pet
                                  {
                                      Id = Guid.NewGuid(),
                                      Age = Convert.ToInt32(petmodel["Age"]),
                                      Breed = new Breed { Name = petmodel["Breed"] },
                                      Name = petmodel["Name"],
                                      Status = new Status() { Name = petmodel["Status"] }
                                  };
                repo.Create(pet);


                return RedirectToAction("Index");
            }
            catch
            {
                return View(petmodel);
            }
        }
    }
}
