using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Calculator(string op, double? a, double? b)
    {
       // var op = Request.Query["op"];
       // var a = double.Parse( Request.Query["a"]);
       // var b = double.Parse(Request.Query["b"]);
       if (a is null || b is null)
       {
           ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b!!!";
           return View("CustomError");
       }
       ViewBag.A = a;
       ViewBag.B = b;
       switch (op)
       {
            case "add" :
                ViewBag.Result = a + b;
                ViewBag.Operator = "+";
                break;
            case "sub" :
                ViewBag.Result = a - b;
                ViewBag.Operator = "-";
                break;
            case "div" :
                ViewBag.Result = a / b;
                ViewBag.Operator = "/";
                break;
            case "mul" :
                ViewBag.Result = a * b;
                ViewBag.Operator = "*";
                break;
            default:
                ViewBag.ErrorMessage = "Nieznany operator!!!";
                return View("CustomError");
        }
        //ViewBag.Result = 1234;
        return View();
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
