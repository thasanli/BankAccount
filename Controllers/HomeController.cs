using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankAccounts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {

        private MyContext db;
        public HomeController(MyContext database)
        {
            db = database;
        }
        
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("registration")]

        public IActionResult Registration(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(db.Users.Any(x => x.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email alrady taken");
                    return View("Index");
                }
            PasswordHasher<User> PwdHash = new PasswordHasher<User>();
            newUser.Password = PwdHash.HashPassword(newUser, newUser.Password);
            db.Add(newUser);
            db.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserID);

            return RedirectToAction("Account");
            }
            return View("Index");
        }
        [HttpGet("Account")]
        public IActionResult Account()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            Console.WriteLine(userID);
            User OnlineUser = db.Users
            .Include(a => a.AllUserTransactions)
            .Where(a => a.UserID == userID)
            .SingleOrDefault();
            OnlineUser.AllUserTransactions = OnlineUser.AllUserTransactions.OrderByDescending(x => x.CreatedAt).ToList();
            
            ViewBag.UserData = OnlineUser;
            ViewBag.User = db.Users.FirstOrDefault(a => a.UserID == (int)HttpContext.Session.GetInt32("UserId"));
            return View();

        }


        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("CheckLogIn")]

        public IActionResult CheckLogIn(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                User CheckEmail = db.Users.FirstOrDefault(x => x.Email == user.LoginEmail);
                if(CheckEmail == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email and/or Password");
                    return View("Login");
                }
                PasswordHasher<User> pwdHash = new PasswordHasher<User>();
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, CheckEmail.Password, user.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email and/or Password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId", CheckEmail.UserID);

                return RedirectToAction("Account");
            }
            return View("Login");
        }

        [HttpPost("makeTransaction")]
        public IActionResult MakeTransaction(Transactions trans)
        {
            User user = db.Users.FirstOrDefault(x => x.UserID == (int)HttpContext.Session.GetInt32("UserId"));
            user.AccountBalace = user.AccountBalace + trans.Amount;
            trans.UserId = user.UserID;
            trans.Creator = user;
            db.AllTransactions.Add(trans);
            db.SaveChanges();
            return RedirectToAction("Account");
        }



        




        public IActionResult Privacy()
        {
            return View();
        }

    }
}
