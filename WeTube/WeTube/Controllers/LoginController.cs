using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using WeTube.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeTube.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterToAPI([Bind("AvatarName,DateOfBirth,Password")] User user)
        {
            user.UserID = 0;
     
            if (ModelState.IsValid)
            {
                //try connecting to webAPI

                //for now, print out the model
                System.Console.WriteLine(user.UserID.ToString(), user.AvatarName, user.DateOfBirth.ToString(), user.Password);
            }
            else
            {
                user.Password = "";
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
