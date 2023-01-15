using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Abby.Web.Pages.Customer.Cart
{
    [Authorize]
	[BindProperties]
    public class SummaryModel : PageModel
    {

        public IEnumerable<ShoppingCard> ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public OrderHeader OrderHeader { get; set; }
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader= new OrderHeader();
            
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCard.GetAll(filter: u => u.ApplicationUserId == claim.Value
                , includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }

                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(t=>t.Id ==claim.Value);
                OrderHeader.PickupName = applicationUser.FirstName + " "+ applicationUser.LastName;
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;  
            }
        }


		public void OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCard.GetAll(filter: u => u.ApplicationUserId == claim.Value
				, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

				foreach (var cartItem in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
				}

				OrderHeader.Status = SD.StatusPending;
				OrderHeader.OrderDate = System.DateTime.Now;
				OrderHeader.UserId = claim.Value;
				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
					OrderHeader.PickUpTime.ToShortTimeString());
				_unitOfWork.OrderHeader.Add(OrderHeader);
				_unitOfWork.Save();

				foreach (var item in ShoppingCartList)
				{
					OrderDetails orderDetails = new()
					{
						MenuItemId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Name = item.MenuItem.Name,
						Price = item.MenuItem.Price,
						Count = item.Count
					};

				}

				_unitOfWork.ShoppingCard.RemoveRange(ShoppingCartList);
				_unitOfWork.Save();
			}
		}
	}
}
