using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;
using System.Text.RegularExpressions;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private IContactManagerRepositiory _repository;

        public ContactController()
            : this(new EntityContactManagerRepository())
        { }

        public ContactController(IContactManagerRepositiory repository)
        {
            _repository = repository;
        }

        // use _entities filed to communicate with database
        // private ContactManagerDBEntities _entities = new ContactManagerDBEntities();

        protected void ValidateContact(Contact contactToCreate)
        {
            if (contactToCreate.FirstName.Trim().Length == 0)
                ModelState.AddModelError("FirstName", "First name is required.");
            if (contactToCreate.LastName.Trim().Length == 0)
                ModelState.AddModelError("LastName", "Last name is required.");
            if (contactToCreate.Phone.Length > 0 && !Regex.IsMatch(contactToCreate.Phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                ModelState.AddModelError("Phone", "Invalid phone number.");
            if (contactToCreate.Email.Length > 0 && !Regex.IsMatch(contactToCreate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                ModelState.AddModelError("Email", "Invalid email address.");
        }

        //
        // GET: /Home/
        //The Index() method returns a view that represents all of the 
        // contacts from the Contacts database table.
        public ActionResult Index()
        {
            return View(_repository.ListContacts());
            // returns the list of contacts as a generic list
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
        public ActionResult Create([Bind(Exclude = "id")] Contact contactToCreate)
        {
            // new section that validates the properties of the Contact class before 
            // the new contact is inserted into the database.
            ValidateContact(contactToCreate);
            if (!ModelState.IsValid)
                return View();

            //database logic
            try
            {
                _repository.CreateContact(contactToCreate);
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
            return View(_repository.GetContact(id));
        }

        //
        // POST: /Home/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contact contactToEdit)
        {
            ValidateContact(contactToEdit);
            if (!ModelState.IsValid)
                return View();

            try
            {
                _repository.EditContact(contactToEdit);
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
            return View(_repository.GetContact(id));
        }

        //
        // POST: /Home/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
