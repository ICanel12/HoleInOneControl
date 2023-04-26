using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HoleInOneControlAPI.Models;
using System.Xml.Linq;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HoleInOneControlAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HandicapsController : Controller
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetPartidas")]
        [HttpGet]
        public async Task<IEnumerable<HoleInOneControlModel.Handicap>> GetPartidas()
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.Handicap> handicaps = await _holeInOneControlContext.Handicaps.Select(h =>
            new HoleInOneControlModel.Handicap
            {
                IdHandicap = h.IdHandicap,
                IdUser = h.IdUser,
                DateHour = h.DateHour,
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
            }
            ).ToListAsync();
            return handicaps;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetPartida")]
        [HttpGet]
        public async Task<HoleInOneControlModel.Handicap> GetPartida(int id)
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            HoleInOneControlModel.Handicap handicap = await _holeInOneControlContext.Handicaps.Select(h =>
            new HoleInOneControlModel.Handicap
            {
                IdHandicap = h.IdHandicap,
                IdUser = h.IdUser,
                DateHour = h.DateHour,
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
            }
            ).FirstOrDefaultAsync(h => h.IdHandicap == id);
            return handicap;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("CreatePartida")]
        [HttpPost]
        public async Task<HoleInOneControlModel.GeneralResult> Create(HoleInOneControlModel.Handicap handicap)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            try
            {
                HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
                Models.Handicap newHandicap = new Models.Handicap
                {
                    IdUser = handicap.IdUser,
                    DateHour = handicap.DateHour,
                    HoleOne = handicap.HoleOne,
                    HoleTwo = handicap.HoleTwo,
                    HoleThree = handicap.HoleThree,
                    HoleFour = handicap.HoleFour,
                    HoleFive = handicap.HoleFive,
                    HoleSix = handicap.HoleSix,
                    HoleSeven = handicap.HoleSeven,
                    HoleEight = handicap.HoleEight,
                    HoleNine = handicap.HoleNine,
                    HoleTen = handicap.HoleTen,
                    HoleEleven = handicap.HoleEleven,
                    HoleTwelve = handicap.HoleTwelve,
                    HoleThirteen = handicap.HoleThirteen,
                    HoleFourteen = handicap.HoleFourteen,
                    HoleFifteen = handicap.HoleFifteen,
                    HoleSixteen = handicap.HoleSixteen,
                    HoleSeventeen = handicap.HoleSeventeen,
                    HoleEighteen = handicap.HoleEighteen
                };
                _holeInOneControlContext.Handicaps.Add(newHandicap);
                await _holeInOneControlContext.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

    }
}
