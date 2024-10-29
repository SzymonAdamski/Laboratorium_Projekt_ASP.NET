using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Servoces;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private readonly IContactServices _contactService;

    public ContactController(IContactServices contactServices)
    {
        _contactService = contactServices;
    }

    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Placek", 
                Email = "adam@wsei.edu.pl",
                BirthDate = new DateOnly(2002, 9, 10),
                PhoneNumber = "999 999 999"
            }
        },
        {
            2, new ContactModel()
            {
                Id = 2,
                FirstName = "Waga",
                LastName = "Placek", 
                Email = "Waga@wsei.edu.pl",
                BirthDate = new DateOnly(2001, 9, 10),
                PhoneNumber = "998 998 999"
            }
        }
    };

    private static int currentId = 2;

    // GET
    public IActionResult Index()
    {
        return View(_contacts.Values.ToList());
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(_contactService);
        }
        _contactService.Update(model);
        return RedirectToAction("Index");
    }

    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _contactService.Add(model);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var contact = _contactService.GetById(id);
        if (contact != null)
        {
            _contactService.Delete(contact);
        }

        return RedirectToAction("Index");
    }

    public ContactModel? GetById(int id)
    {
        return _contactService.GetById(id);
    }
}
