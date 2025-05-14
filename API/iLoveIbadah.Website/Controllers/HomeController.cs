using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using iLoveIbadah.Website.Models;

namespace iLoveIbadah.Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // --- Action method placeholder for the waitlist form ---
    [HttpPost]
    [ValidateAntiForgeryToken] // Good practice for security
    public IActionResult JoinWaitlist(string email) // Parameter name matches input name
    {
        if (ModelState.IsValid && !string.IsNullOrEmpty(email))
        {
            // TODO: Add logic here to save the email address to your waitlist
            // (e.g., database, file, third-party service)
            System.Diagnostics.Debug.WriteLine($"Waitlist Only Sign up: {email}"); // Example logging

            // Redirect to a thank you page or back to Index with a success message
            TempData["WaitlistMessage"] = "Thanks for joining the waitlist!";
            return RedirectToAction("Index");
        }
        // If model state is invalid or email is empty, return to the form
        TempData["WaitlistError"] = "Please enter a valid email address.";
        return RedirectToAction("Index"); // Or return View("Index"); if you pass model errors
    }

    // --- Action method placeholder for the waitlist + newsletter form ---
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult JoinWaitlistAndNewsletter(string email) // Parameter name matches input name
    {
        if (ModelState.IsValid && !string.IsNullOrEmpty(email))
        {
            // TODO: Add logic here to save the email address to your waitlist AND newsletter list
            System.Diagnostics.Debug.WriteLine($"Waitlist & Newsletter Sign up: {email}"); // Example logging

            TempData["WaitlistMessage"] = "Thanks for joining the waitlist and newsletter!";
            return RedirectToAction("Index");
        }
        TempData["WaitlistError"] = "Please enter a valid email address.";
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
