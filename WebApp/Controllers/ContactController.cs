using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactServices _contactService;

        public ContactController(IContactServices contactService)
        {
            _contactService = contactService;
        }

        // Lista kontaktów
        public ActionResult Index()
        {
            var contacts = _contactService.GetAll();
            return View(contacts);
        }

        // Wyświetlenie formularza dodawania kontaktu
        public ActionResult Add()
        {
            var model = CreateContactModel();
            return View(model);
        }

        // Odebranie danych z formularza i zapisanie w kontaktach
        [HttpPost]
        public ActionResult Add(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Organizations = GetOrganizationsSelectList();
                return View(model);
            }

            _contactService.Add(model);
            return RedirectToAction("Index");
        }

        // Usunięcie kontaktu
        public ActionResult Delete(int id)
        {
            _contactService.Delete(id);
            return RedirectToAction("Index");
        }

        // Szczegóły kontaktu
        public ActionResult Details(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // Edycja kontaktu
        public ActionResult Edit(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.Organizations = GetOrganizationsSelectList();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Organizations = GetOrganizationsSelectList();
                return View(model);
            }

            _contactService.Update(model);
            return RedirectToAction("Index");
        }

        // Metody pomocnicze
        private ContactModel CreateContactModel()
        {
            return new ContactModel
            {
                Organizations = GetOrganizationsSelectList()
            };
        }

        private List<SelectListItem> GetOrganizationsSelectList()
        {
            return _contactService.GetOrganization()
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                })
                .ToList();
        }
    }
}
