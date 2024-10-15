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
    
    public IActionResult Calculator(Operator? op, double? a, double? b)
    {
       // var op = Request.Query["op"];
       // var a = double.Parse( Request.Query["a"]);
       // var b = double.Parse(Request.Query["b"]);
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
            case Operator.Add :
                ViewBag.Result = a + b;
                ViewBag.Operator = "+";
                break;
            case Operator.Sub :
                ViewBag.Result = a - b;
                ViewBag.Operator = "-";
                break;
            case Operator.Div :
                ViewBag.Result = a / b;
                ViewBag.Operator = "/";
                break;
            case Operator.Mul :
                ViewBag.Result = a * b;
                ViewBag.Operator = "*";
                break;
        }
        //ViewBag.Result = 1234;
        return View();
    }

    public IActionResult Age( DateTime datauro, DateTime datatera)
    {
        ViewBag.urodziny = datauro;
        ViewBag.teraz = datatera;
        int age =datatera.Year - datauro.Year ;
        if (datatera.Month < datauro.Month)
        {
            age --;
        }

        ViewBag.Result = age;
        
        

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

public enum Operator
{
    Add,Sub,Div,Mul,
}