using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HoleInOneControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace HoleInOneControl.Controllers
{
	public class HandicapsController : Controller
	{
        HoleInOneControlContext _holeInOneContext = new HoleInOneControlContext();

        [Authorize]
        public IActionResult Index()
		{
			return View();
		}


		[HttpGet]
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
            IEnumerable<HoleInOneControlModel.Handicap> handicap = await Functions.APIServices.HandicapList(token.token);
			return View(handicap);
		}


        [Authorize]
        public IActionResult Create()
       {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.User> users = (from u in _holeInOneControlContext.Users
                                                             select new HoleInOneControlModel.User
                                                             {
                                                                 IdUser = u.IdUser,
                                                                 UserName = u.UserName
                                                             }).ToList();

            HoleInOneControlModel.Handicap handicap= new HoleInOneControlModel.Handicap();
            handicap.Users = users.Select(s => new SelectListItem()
            {
                Value = s.IdUser.ToString(),
                Text = s.UserName
            }).ToList();

            return View(handicap);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("IdUser,HoleOne,HoleTwo,HoleThree,HoleFour,HoleFive,HoleSix,HoleSeven,HoleEight,HoleNine,HoleTen,HoleEleven,HoleTwelve,HoleThirteen,HoleFourteen,HoleFifteen,HoleSixteen,HoleSeventeen,HoleEighteen,valorHandicap, userName")] HoleInOneControlModel.Handicap handicap)
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
            await Functions.APIServices.CreateHandicap(handicap, token.token);

         

            return RedirectToAction(nameof(List));
        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            Models.Handicap handicap = _holeInOneContext.Handicaps.Find(id);

            return View(handicap);
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteRegister(int IdHandicap)
        {
            // Buscar el artículo existente en la base de datos
            Models.Handicap handicap = _holeInOneContext.Handicaps.Find(IdHandicap);

            if (handicap == null)
            {
                // Si el artículo no existe, devolver un mensaje de error
                return NotFound();
            }

            try
            {
                // Eliminar el artículo de la base de datos
                _holeInOneContext.Handicaps.Remove(handicap);
                _holeInOneContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // Si se produce un error al eliminar el artículo, devolver un mensaje de error
                ModelState.AddModelError("", "No se pudo eliminar la partida. Inténtelo de nuevo.");
                return View(handicap);
            }

            // Si todo va bien, redirigir al usuario a la lista de artículos
            return RedirectToAction("List");
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            Models.Handicap handicap = _holeInOneContext.Handicaps.Find(id);
            return View(handicap);
        }



        [HttpPost]
        [Authorize]
        public IActionResult Edit(int idhandicap, int holeone, int holetwo, int holethree, int holefour, int holefive, int holesix, int holeseven, int holeeight, int holenine, int holeten, int holeeleven, int holetwelve, int holethirteen, int holefourteen, int holefifteen, int holesixteen, int holeseventeen, int holeeighteen)
        {
            Models.Handicap handicap = _holeInOneContext.Handicaps.Find(idhandicap);
            string message = "", errorMessage = "";

            if (handicap == null)
            {
                // Si el artículo no existe, devolver un mensaje de error
                return NotFound();
            }

            // Actualizar las propiedades del artículo con los valores proporcionados en los parámetros
            handicap.HoleOne = holeone;
            handicap.HoleTwo = holetwo;
            handicap.HoleThree = holethree;
            handicap.HoleFour = holefour;
            handicap.HoleFive = holefive;
            handicap.HoleSix = holesix;
            handicap.HoleSeven = holeseven;
            handicap.HoleEight = holeeight;
            handicap.HoleNine = holenine;
            handicap.HoleTen = holeten;
            handicap.HoleEleven = holeeleven;
            handicap.HoleTwelve = holetwelve;
            handicap.HoleThirteen = holethirteen;
            handicap.HoleFourteen = holefourteen;
            handicap.HoleFifteen = holefifteen;
            handicap.HoleSixteen = holesixteen;
            handicap.HoleSeventeen = holeseventeen;
            handicap.HoleEighteen = holeeighteen;

            try
            {
                // Guardar los cambios en la base de datos
                _holeInOneContext.SaveChanges();
                message = "Los cambios se han guardado correctamente.";
                return RedirectToAction("List");
            }
            catch (DbUpdateException)
            {
                // Si se produce un error al guardar los cambios, devolver un mensaje de error
                ModelState.AddModelError("", "No se pudieron guardar los cambios. Inténtelo de nuevo.");
                errorMessage = "No se ha podido editar la partida.";
                return View(handicap);
            }

        }


        [Authorize]
        public IActionResult CalcularHandicap()
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.User> users = (from u in _holeInOneControlContext.Users
                                                             select new HoleInOneControlModel.User
                                                             {
                                                                 IdUser = u.IdUser,
                                                                 UserName = u.UserName
                                                             }).ToList();

            HoleInOneControlModel.Handicap handicap = new HoleInOneControlModel.Handicap();
            handicap.Users = users.Select(s => new SelectListItem()
            {
                Value = s.IdUser.ToString(),
                Text = s.UserName
            }).ToList();

            return View(handicap);
        }


        [Authorize]
        public IActionResult CalcularValorHandicap(int idUser)
        {

            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            HoleInOneControlModel.Handicap datos = new HoleInOneControlModel.Handicap();
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.User> users = (from u in _holeInOneControlContext.Users
                                                             select new HoleInOneControlModel.User
                                                             {
                                                                 IdUser = u.IdUser,
                                                                 UserName = u.UserName
                                                             }).ToList();

            HoleInOneControlModel.Handicap handicap = new HoleInOneControlModel.Handicap();
            datos.Users = users.Select(s => new SelectListItem()
            {
                Value = s.IdUser.ToString(),
                Text = s.UserName
            }).ToList();

            IEnumerable<HoleInOneControlModel.User> user = (from u in _holeInOneControlContext.Users
                                                             where u.IdUser == idUser
                                                             select new HoleInOneControlModel.User
                                                             {
                                                                 UserName = u.UserName
                                                             }).ToList();


            IEnumerable<HoleInOneControlModel.Handicap> promedio = (from h in _holeInOneControlContext.Handicaps
                                                            where h.IdUser == idUser && h.DateHour >= firstDayOfMonth && h.DateHour <= lastDayOfMonth
                                                                    select new HoleInOneControlModel.Handicap
                                                            {                                                          
                                                                HoleOne = h.HoleOne,
                                                                HoleTwo = h.HoleTwo,
                                                                HoleThree = h.HoleThree,
                                                                HoleFour = h.HoleFour,
                                                                HoleFive = h.HoleFive,
                                                                HoleSix = h.HoleSix,
                                                                HoleSeven = h.HoleSeven,
                                                                HoleEight = h.HoleEight,
                                                                HoleNine = h.HoleNine,
                                                                HoleTen = h.HoleTen,
                                                                HoleEleven = h.HoleEleven,
                                                                HoleTwelve = h.HoleTwelve,
                                                                HoleThirteen = h.HoleThirteen,
                                                                HoleFourteen = h.HoleFourteen,
                                                                HoleFifteen = h.HoleFifteen,
                                                                HoleSixteen = h.HoleSixteen,
                                                                HoleSeventeen = h.HoleSeventeen,
                                                                HoleEighteen = h.HoleEighteen
                                                        }).ToList();

            int promedioTotal = 0;
            int suma = 0;

            foreach (var valor in promedio)
            {
                suma = valor.HoleOne.Value + valor.HoleTwo.Value + valor.HoleThree.Value + valor.HoleFour.Value + valor.HoleFive.Value + valor.HoleSix.Value + valor.HoleSeven.Value + valor.HoleEight.Value + valor.HoleNine.Value + valor.HoleTen.Value + valor.HoleEleven.Value + valor.HoleTwelve.Value + valor.HoleThirteen.Value + valor.HoleFourteen.Value + valor.HoleFifteen.Value + valor.HoleSixteen.Value + valor.HoleSeventeen.Value + valor.HoleEighteen.Value;
            }

            promedioTotal = suma / promedio.Count();

            datos.valorHandicap = promedioTotal;
            datos.userName = user.FirstOrDefault()?.UserName;

            return View("CalcularHandicap", datos);
        }

        

    }
}
