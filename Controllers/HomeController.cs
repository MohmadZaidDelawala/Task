using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task.Models;

namespace Task.Controllers;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Sign-Up")]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpGet("Products")]
    public IActionResult ProductMaster()
    {
        return View();
    }

}
