using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HoleInOneControlAPI.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HoleInOneControlAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsersController : Controller
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetUsers")]
        [HttpGet]
        public async Task<IEnumerable<HoleInOneControlModel.User>> GetUsers()
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.User> users = await _holeInOneControlContext.Users.Select(u =>
            new HoleInOneControlModel.User
            {
                IdUser = u.IdUser,
                UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName,
                Password = u.Password
            }
            ).ToListAsync();
            return users;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetUser")]
        [HttpGet]
        public async Task<HoleInOneControlModel.User> GetUser(int id)
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            HoleInOneControlModel.User user = await _holeInOneControlContext.Users.Select(u =>
            new HoleInOneControlModel.User
            {
                IdUser = u.IdUser,
                UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName,
                Password = u.Password
            }
            ).FirstOrDefaultAsync(s => s.IdUser == id);
            return user;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("CreateUser")]
        [HttpPost]
        public async Task<HoleInOneControlModel.GeneralResult> Create(HoleInOneControlModel.User user)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            try
            {
                HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
                Models.User newUser = new Models.User
                {
                    UserName = user.UserName,
                    Name = user.Name,
                    LastName = user.LastName,
                    Password = user.Password
                };
                _holeInOneControlContext.Users.Add(newUser);
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



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("UpdateUser")]
        [HttpPut]
        public async Task<HoleInOneControlModel.GeneralResult> Update(HoleInOneControlModel.User user)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            try
            {
                HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
                var userToUpdate = await _holeInOneControlContext.Users.FindAsync(user.IdUser);
                if (userToUpdate != null)
                {
                    userToUpdate.UserName = user.UserName;
                    userToUpdate.Name = user.Name;
                    userToUpdate.LastName = user.LastName;
                    userToUpdate.Password = user.Password;

                    await _holeInOneControlContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
                else
                {
                    generalResult.ErrorMessage = $"Usuario no encontrado.";
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }



        //Probarlo como Post o utilizar un modelo
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<HoleInOneControlModel.GeneralResult> DeleteUser(int idUser)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            using (var dbContext = new HoleInOneControlContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var regionToDelete = await dbContext.Users.Include(r => r.Articles).FirstAsync(r => r.IdUser == idUser);

                        if (regionToDelete != null)
                        {

                            dbContext.Users.Remove(regionToDelete);

                            dbContext.SaveChanges();

                            transaction.Commit();
                        }
                        else
                        {
                            generalResult.ErrorMessage = $"Usuario no encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return generalResult;
        }


    }
}
