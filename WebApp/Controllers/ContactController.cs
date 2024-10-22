using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                FirstName = "adam",
                LasttName = "placek",
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
                LasttName = "placek",
                Email = "Waga@wsei.edu.pl",
                BirthDate = new DateOnly(2001, 9, 10),
                PhoneNumber = "998 998 999"
            }

        },

    };

    private static int currentId = 2;
    // GET
    public IActionResult Index()
    {
        return View(_contacts);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        model.Id = ++currentId;
        _contacts.Add( model.Id, model);
        return View("index",_contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }
}