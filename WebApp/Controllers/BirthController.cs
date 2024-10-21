using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BirthController : Controller
    {
        // Wyświetla formularz
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result([FromForm] Birth model)
        {
            if (!model.IsValid())
            {
                ViewBag.ErrorMessage = "Niepoprawne dane. Upewnij się, że imię, nazwisko zostały podane, a data urodzenia jest wcześniejsza od daty bieżącej.";
                return View("CustomError");
            }

            return View(model);
        }
    }
}