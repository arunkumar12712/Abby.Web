using Abby.DataAccess.Repository;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abby.Web.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailsVM OrderDetailsVM { get; set; }
        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            OrderDetailsVM = new()
            {
                OrderHeader= _unitOfWork.OrderHeader.GetFirstOrDefault(x => x.Id == id, includeProperties:"ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(x => x.OrderId == id).ToList()
            };
        }
    }
}
