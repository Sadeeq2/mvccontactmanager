using System.Web.Mvc;
using ContactManager.Models.Validation;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private IContactManagerService _service;

        public ContactController()
        {
            _service = new ContactManagerService(new ModelStateWrapper(this.ModelState));
        }

        public ContactController(IContactManagerService service)
        {
            _service = service;
        }

        //
        // GET: /Home/
        //The Index() method returns a view that represents all of the 
        // contacts from the Contacts database table.
        public ActionResult Index()
        {
            return View(_service.ListContacts());
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
            if (_service.CreateContact(contactToCreate))
                return RedirectToAction("Index");
            return View();
        }

        //
        // GET: /Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_service.GetContact(id));
        }

        //
        // POST: /Home/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contact contactToEdit)
        {
            if (_service.EditContact(contactToEdit))
                return RedirectToAction("Index");
            return View();
        }

        //
        // GET: /Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_service.GetContact(id));
        }

        //
        // POST: /Home/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            if (_service.DeleteContact(contactToDelete))
                return RedirectToAction("Index");
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
