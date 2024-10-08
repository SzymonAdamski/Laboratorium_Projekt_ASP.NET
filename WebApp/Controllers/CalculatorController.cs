using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: CalculatorController
        public IActionResult Form()
        {
            return View();
        }
        public IActionResult Result(Operator? op, double? a, double? b)
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
            ViewBag.A = a;
            ViewBag.B = b;
            switch (op)
            {
                case Operator.Add:
                    ViewBag.Result = a + b;
                    ViewBag.Operator = "+";
                    break;
                case Operator.Sub:
                    ViewBag.Result = a - b;
                    ViewBag.Operator = "-";
                    break;
                case Operator.Div:
                    ViewBag.Result = a / b;
                    ViewBag.Operator = "/";
                    break;
                case Operator.Mul:
                    ViewBag.Result = a * b;
                    ViewBag.Operator = "*";
                    break;
            }
            return View();
        }
        

    }
}
