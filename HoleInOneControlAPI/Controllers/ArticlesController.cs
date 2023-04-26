using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HoleInOneControlAPI.Models;
using System.Xml.Linq;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HoleInOneControlAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetArticles")]
        [HttpGet]
        public async Task<IEnumerable<HoleInOneControlModel.Article>> GetArticles()
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.Article> articles = await _holeInOneControlContext.Articles.Select(a =>
            new HoleInOneControlModel.Article
            {
                IdArticle = a.IdArticle,
                IdUser = a.IdUser,
                NameArticle = a.NameArticle,
                Brand = a.Brand,
                Model = a.Model,
                Capacity = a.Capacity,
                Color = a.Color,
                Type = a.Type,
                Material = a.Material,
                Description = a.Description
            }
            ).ToListAsync();
            return articles;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetArticle")]
        [HttpGet]
        public async Task<HoleInOneControlModel.Article> GetArticle(int id)
        {
            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
            HoleInOneControlModel.Article article = await _holeInOneControlContext.Articles.Select(a =>
            new HoleInOneControlModel.Article
            {
                IdArticle = a.IdArticle,
                IdUser = a.IdUser,
                NameArticle = a.NameArticle,
                Brand = a.Brand,
                Model = a.Model,
                Capacity = a.Capacity,
                Color = a.Color,
                Type = a.Type,
                Material = a.Material,
                Description = a.Description
            }
            ).FirstOrDefaultAsync(a => a.IdArticle == id);
            return article;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("CreateArticle")]
        [HttpPost]
        public async Task<HoleInOneControlModel.GeneralResult> Create(HoleInOneControlModel.Article article)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            try
            {
                HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
                Models.Article newArticle = new Models.Article
                {                
                    IdUser = article.IdUser,
                    NameArticle = article.NameArticle,
                    Brand = article.Brand,
                    Model = article.Model,
                    Capacity = article.Capacity,
                    Color = article.Color,
                    Type = article.Type,
                    Material = article.Material,
                    Description = article.Description

                };
                _holeInOneControlContext.Articles.Add(newArticle);
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
        [Route("UpdateArticle")]
        [HttpPut]
        public async Task<HoleInOneControlModel.GeneralResult> Update(HoleInOneControlModel.Article article)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            try
            {
                HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();
                var articleToUpdate = await _holeInOneControlContext.Articles.FindAsync(article.IdArticle);
                if (articleToUpdate != null)
                {
                    articleToUpdate.IdUser = article.IdUser;
                    articleToUpdate.NameArticle = article.NameArticle;
                    articleToUpdate.Brand = article.Brand;
                    articleToUpdate.Model = article.Model;
                    articleToUpdate.Capacity = article.Capacity;
                    articleToUpdate.Color = article.Color;
                    articleToUpdate.Type = article.Type;
                    articleToUpdate.Material = article.Material;
                    articleToUpdate.Description = article.Description;

                    await _holeInOneControlContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
                else
                {
                    generalResult.ErrorMessage = $"Artículo no encontrado.";
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("DeleteArticle")]
        [HttpDelete]
        public async Task<HoleInOneControlModel.GeneralResult> Delete(int idArticle)
        {
            HoleInOneControlModel.GeneralResult generalResult = new HoleInOneControlModel.GeneralResult()
            {
                Result = false
            };

            HoleInOneControlContext _holeInOneControlContext = new HoleInOneControlContext();

            try
            {

                var article = await _holeInOneControlContext.Articles.FindAsync(idArticle);
                if (article != null)
                {
                    _holeInOneControlContext.Articles.Remove(article);
                    await _holeInOneControlContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
                else
                {
                    generalResult.ErrorMessage = $"Artículo no encontrado.";
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }



    }
}
