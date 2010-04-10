using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Model;

namespace Web.Controllers
{
    public class PublishController : Controller
    {
        //
        // GET: /Publish/

        public ActionResult Index()
        {
            return View(new List<Pet>{new Pet()});
        }

        //
        // GET: /Publish/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Publish/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Publish/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Publish/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Publish/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Publish/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Publish/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
