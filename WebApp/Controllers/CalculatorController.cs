using Microsoft.AspNetCore.Mvc;
using WebApp.Models; 

namespace WebApp.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: CalculatorController/Form
        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Result(Operators? op, double? a, double? b)
        {
            if (a is null || b is null)
            {
                ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b!!!";
                return View("CustomError");
            }

            if (op is null)
            {
                ViewBag.ErrorMessage = "Nieznany operator!!!";
                return View("CustomError");
            }

            var calculator = new Calculator
            {
                a = a,
                b = b,
                Operator = op
            };

            return View(calculator);
        }
    }
}