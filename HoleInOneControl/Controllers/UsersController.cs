using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HoleInOneControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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


        [Authorize]
        public async Task<IActionResult> List()
        {
            HoleInOneControlModel.Token token = await Functions.APIServices.LoginAPILogin(
            new HoleInOneControlModel.Token
            {
                token = "adfadsfadsfasd"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }
            IEnumerable<HoleInOneControlModel.User> users = await Functions.APIServices.UsersGetList(token.token);
            return View(users);
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("UserName,Name,LastName,Password")] HoleInOneControlModel.User user) 
        {
            if (ModelState.IsValid) 
            {
                HoleInOneControlModel.Token token = await Functions.APIServices.LoginAPILogin(
                new HoleInOneControlModel.Token
                {
                    token = "adfadsfadsfasd"
                });

                if (string.IsNullOrEmpty(token.token))
                {
                    return NotFound();
                }
                await Functions.APIServices.UserSet(user, token.token);
            
            }

            return RedirectToAction(nameof(List));
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            Models.User user = _holeInOneContext.Users.Find(id);
            return View(user);
        }


        [HttpPost]
        [Authorize]
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


        [Authorize]
        public ActionResult Delete(int id)
        {
            Models.User user = _holeInOneContext.Users.Find(id);
            return View(user);
        }


        [Authorize]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int IdUser)
        {
            // Buscar el artículo existente en la base de datos
            Models.User user = _holeInOneContext.Users.Find(IdUser);

            if (user == null)
            {
                // Si el artículo no existe, devolver un mensaje de error
                return NotFound();
            }

            try
            {
                // Eliminar el artículo de la base de datos
                _holeInOneContext.Users.Remove(user);
                _holeInOneContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // Si se produce un error al eliminar el artículo, devolver un mensaje de error
                ModelState.AddModelError("", "No se pudo eliminar el artículo. Inténtelo de nuevo.");
                return View(user);
            }

            // Si todo va bien, redirigir al usuario a la lista de artículos
            return RedirectToAction("List");
        }
    

    }
}
