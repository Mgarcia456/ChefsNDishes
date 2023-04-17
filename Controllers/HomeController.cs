using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //Read all chefs (get)
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> AllChefs = _context.Chefs.Include(d => d.CreatedDishs).ToList();
        return View(AllChefs);
    }

    //Read all dishes (get)
    [HttpGet("dishes")]
    public IActionResult ViewDish()
    {
        List<Dish> AllDishes = _context.Dishes.Include(c => c.Chef).ToList();
        return View(AllDishes);
    }

    //form for adding/creating a chef (get)
    [HttpGet("chefs/new")]
    public IActionResult AddChef()
    {
        return View();
    }

    //form for adding/creating a dish (get)
    [HttpGet("dishes/new")]
    public IActionResult AddDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }

    //route for actually adding/creating chef (post)
    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("AddChef");
        }
    }

    //route for actually adding/creating dish (post)
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("ViewDish");
        }
        else
        {
            ViewBag.AllChefs = _context.Chefs.ToList();
            return View("AddDish");
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}