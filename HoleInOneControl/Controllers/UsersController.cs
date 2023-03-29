using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HoleInOneControl.Models;
using Microsoft.EntityFrameworkCore;

namespace HoleInOneControl.Controllers
{
    public class UsersController : Controller
    {
        HoleInOneControlContext _holeInOneContext = new HoleInOneControlContext();

        // GET: UsersController
        public ActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> List()
        {
            IEnumerable<HoleInOneControlModel.User> users = await Functions.APIServices.UsersGetList();
            return View(users);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserName,Name,LastName,Password")] HoleInOneControlModel.User user) 
        {
            if (ModelState.IsValid) 
            {
                await Functions.APIServices.UserSet(user);
            
            }

            return RedirectToAction(nameof(List));
        }


        public ActionResult Edit(int id)
        {
            Models.User user = _holeInOneContext.Users.Find(id);
            return View(user);
        }


        [HttpPost]
        public IActionResult Edit(int Id, string UserName, string Name, string LastName, string Password)
        {
            Models.User user = _holeInOneContext.Users.Find(Id);
            string message = "", errorMessage = "";

            if (user == null)
            {
                // Si el artículo no existe, devolver un mensaje de error
                return NotFound();
            }

            // Actualizar las propiedades del artículo con los valores proporcionados en los parámetros
            user.UserName = UserName;
            user.Name = Name;
            user.LastName = LastName;
            user.Password = Password;

            try
            {
                // Guardar los cambios en la base de datos
                _holeInOneContext.SaveChanges();
                message = "Los cambios se han guardado correctamente.";
            }
            catch (DbUpdateException)
            {
                // Si se produce un error al guardar los cambios, devolver un mensaje de error
                ModelState.AddModelError("", "No se pudieron guardar los cambios. Inténtelo de nuevo.");
                errorMessage = "No se ha podido editar el artículo.";
                return View(user);
            }

            ViewData["Message"] = message;
            ViewData["ErrorMessage"] = errorMessage;
            return View();
        }


        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            Models.User user = _holeInOneContext.Users.Find(id);
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(int IdUser)
        {
            if (ModelState.IsValid)
            {
                await Functions.APIServices.DeleteUser(IdUser);

            }

            return RedirectToAction(nameof(List));
        }

    }
}
