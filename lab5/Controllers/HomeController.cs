using lab5.Areas.Identity.Data;
using lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<lab5User> _userManager;
        private readonly SignInManager<lab5User> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<lab5User> userManager, SignInManager<lab5User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
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
        [Authorize]
        public async Task<ActionResult> Block(string[] checkUser)
        {

            foreach (var id in checkUser)
            {
                var user = await _userManager.FindByIdAsync(id);
                user.IsBlocked = true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.SetLockoutEndDateAsync(user, new DateTime(9999, 12, 30));
                    lab5User iuser = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (iuser == user)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("index");
                    }
                }
            }
            return RedirectToAction("index");
        }
        [Authorize]
        public async Task<ActionResult> UnBlock(string[] checkUser)
        {
            foreach (var id in checkUser)
            {
                var user = await _userManager.FindByIdAsync(id);
                user.IsBlocked = false;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTime.Now - TimeSpan.FromMinutes(1));
                }
            }
            return RedirectToAction("index");
        }
        [Authorize]
        public async Task<ActionResult> Delete(string[] checkUser)
        {
            foreach (var id in checkUser)
            {
                var user = await _userManager.FindByIdAsync(id);
                lab5User iuser = await _userManager.FindByNameAsync(User.Identity.Name);

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    if (iuser == user)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("index");
                    }
                }
            }
            return RedirectToAction("index");
        }
    }
}
