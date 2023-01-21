using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Abby.Web.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCard ShoppingCard { get; set; }
       

        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCard = new ShoppingCard
            {
                 ApplicationUserId = claim.Value,
                 MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType"),
                 MenuItemId = id            
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                 ShoppingCard shoppingCardFromDb = _unitOfWork.ShoppingCard.GetFirstOrDefault(
                    filter: u => u.ApplicationUserId == ShoppingCard.ApplicationUserId &&
                     u.MenuItemId == ShoppingCard.MenuItemId
                    );
                
                if (shoppingCardFromDb == null)
                {
                    _unitOfWork.ShoppingCard.Add(ShoppingCard);
                    _unitOfWork.Save();
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCard.GetAll(u => u.ApplicationUserId == ShoppingCard.ApplicationUserId).ToList().Count);

                }
                else
                {
                    _unitOfWork.ShoppingCard.IncrementCount(shoppingCardFromDb, ShoppingCard.Count);
                }

                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
