using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HoleInOneControl.Models;

namespace HoleInOneControl.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
 
            EncryptMD5 encr = new EncryptMD5();
            string passwordEncrypt = encr.Encrypt(password);

            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();

            IEnumerable<HoleInOneControlModel.User> users = (from u in _holeInOneControlContext.Users
                                                             where u.UserName == userName && u.Password == passwordEncrypt
                                                             select new HoleInOneControlModel.User
                                                             {
                                                                 UserName = u.UserName,
                                                                 Password = u.Password
                                                             }).ToList();
            
            if(users.Count() == 1)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", userName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "1234"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
