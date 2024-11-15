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
            return View(_contactService.GetAll());
        }
        
        //Dodanie kontaktu formularz 
        public ActionResult Add()
        {
            var model = new ContactModel();
            model.Organizations = _contactService.GetOrganization()
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                    Selected = i.Id == 1
                })
                .ToList();
            return View();
        }

        //Odebranie danych z formularza i zapisanie w kontaktach
        [HttpPost]
        public ActionResult Add(ContactModel model)
        {
           var model = new ContactModel();
            model.Organizations = _contactService.GetOrganization()
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                    Selected = i.Id == 1
                })
                .ToList();
            return View();
        }

        public ActionResult Delete(int id)
        {
            _contactService.Delete(id);
            return View("Index");
        }

        public ActionResult Details(int id)
        {
            return View(_contactService.GetById(id));
        }
        
        public ActionResult Edit(int id)
        {
            return View(_contactService.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _contactService.Update(model);
            return View("Index");
        }
    }
}
