using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Data;
using Abby.Models;
using Abby.DataAccess.Repository.IRepository;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodType FoodType { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
           
            if (ModelState.IsValid)
            {
                 _unitOfWork.FoodType.Add(FoodType);
                 _unitOfWork.Save();
                TempData["success"] = "FoodType create successfully";
                return RedirectToPage("Index");
            }
            
            return Page();
        }

    }
}
