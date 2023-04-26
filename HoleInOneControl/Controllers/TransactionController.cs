using HoleInOneControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoleInOneControl.Controllers
{
    public class TransactionController : Controller
    {

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> List()
        {
            HoleInOneControlContext _transactionContext = new HoleInOneControlContext();
            IEnumerable<HoleInOneControlModel.Transaction> transactions =
                (from mc in _transactionContext.Transactions
                 join m in _transactionContext.Users on mc.IdUser equals m.IdUser
            
                 select new HoleInOneControlModel.Transaction
                 {
                     UserName = m.UserName,
                     DateHour = mc.DateHour,
                     TypeTransaction = mc.TypeTransaction,
                     NameArticle = mc.NameArticle,
                     Brand = mc.Brand,
                     Model = mc.Model,
                     Capacity = mc.Capacity,
                     Color = mc.Color,
                     Type = mc.Type,
                     Material = mc.Material,
                     Description = mc.Description
                 }).ToList();
            return View(transactions);

        }
    }
}
