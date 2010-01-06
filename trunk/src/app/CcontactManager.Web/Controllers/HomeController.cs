using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        // use _entities filed to communicate with database
        private ContactManagerDBEntities _entities = new ContactManagerDBEntities();

        //
        // GET: /Home/
        //The Index() method returns a view that represents all of the 
        // contacts from the Contacts database table.
        public ActionResult Index()
        {
            return View(_entities.ContactSet.ToList());
            // returns the list of contacts as a generic list
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="id")] Contact contactToCreate)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                _entities.AddToContactSet(contactToCreate);
                _entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            var contactToEdit = (from c in _entities.ContactSet
                                 where c.ID == id
                                 select c).FirstOrDefault();
            return View(contactToEdit);
        }

        //
        // POST: /Home/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contact contactToEdit)
         {   
            if (!ModelState.IsValid)
                return View();
       
            try
            {
                var originalContact = (from c in _entities.ContactSet
                                       where c.ID == contactToEdit.ID
                                       select c).FirstOrDefault();

                _entities.ApplyPropertyChanges(originalContact.EntityKey.EntitySetName, contactToEdit);
                _entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5
        public ActionResult Delete(int id)
        {
            var contactToDelete = (from c in _entities.ContactSet
                                   where c.ID == id
                                   select c).FirstOrDefault();
            return View(contactToDelete);
        }

        //
        // POST: /Home/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            try
            {
                var originalContact = (from c in _entities.ContactSet
                                       where c.ID == contactToDelete.ID
                                       select c).FirstOrDefault();
                _entities.DeleteObject(originalContact);
                _entities.SaveChanges();
                return RedirectToAction("Index");

            }
            catch 
            {
                return View();
            }
        }
    }
}
