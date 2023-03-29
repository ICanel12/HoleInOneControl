using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HoleInOneControl.Models;
using Microsoft.EntityFrameworkCore;

namespace HoleInOneControl.Controllers
{
    public class ArticlesController : Controller
    {
        HoleInOneControlContext _holeInOneContext = new HoleInOneControlContext();
        public IActionResult Index()
        {
            return View();
        }

        // GET: CategoriesController/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Models.Article article = _holeInOneContext.Articles.Find(id);
            return View(article);
        }



        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdUser,NameArticle,Brand,Model,Capacity,Color,Type,Material,Description")] HoleInOneControlModel.Article article)
        {
            if (ModelState.IsValid)
            {
                await Functions.APIServices.ArticleSet(article);

            }

            return RedirectToAction(nameof(List));
        }


        [HttpPost]
        public IActionResult Edit(int id, int iduser, string namearticle, string brand, string model, int capacity, string color, string type, string material, string description)
        {
            Models.Article article = _holeInOneContext.Articles.Find(id);
            string message ="", errorMessage="";

            if (article == null)
            {
                // Si el artículo no existe, devolver un mensaje de error
                return NotFound();
            }

            // Actualizar las propiedades del artículo con los valores proporcionados en los parámetros
            article.IdUser = iduser;
            article.NameArticle = namearticle;
            article.Brand = brand;
            article.Model = model;
            article.Capacity = capacity;
            article.Color = color;
            article.Type = type;
            article.Material = material;
            article.Description = description;

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
                return View(article);
            }

            ViewData["Message"] = message;
            ViewData["ErrorMessage"] = errorMessage;
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<HoleInOneControlModel.Article> articles = await Functions.APIServices.ArticlesGetList();
            return View(articles);
        }


        public IActionResult Delete(int id)
        {
            Models.Article article = _holeInOneContext.Articles.Find(id);

            return View(article);
        }



        [HttpPost]
        public IActionResult DeleteRegister(int IdArticle)
        {
            // Buscar el artículo existente en la base de datos
            Models.Article article = _holeInOneContext.Articles.Find(IdArticle);

            if (article == null)
            {
                // Si el artículo no existe, devolver un mensaje de error
                return NotFound();
            }

            try
            {
                // Eliminar el artículo de la base de datos
                _holeInOneContext.Articles.Remove(article);
                _holeInOneContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // Si se produce un error al eliminar el artículo, devolver un mensaje de error
                ModelState.AddModelError("", "No se pudo eliminar el artículo. Inténtelo de nuevo.");
                return View(article);
            }

            // Si todo va bien, redirigir al usuario a la lista de artículos
            return RedirectToAction("List");
        }

    }
}
