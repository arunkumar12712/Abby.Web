using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Data;
using Abby.Models;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public FoodType FoodType { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public void OnGet(int id)
        {
            FoodType = _db.FoodType.Find(id);

        }


        public async Task<IActionResult> OnPost()
        {
            
                var foodTypeFromDb = _db.Category.Find(FoodType.Id);
                if(foodTypeFromDb != null)
                {
                    _db.Category.Remove(foodTypeFromDb);
                    await _db.SaveChangesAsync();
                TempData["success"] = "Food Type delete successfully";
                return RedirectToPage("Index");
            }
                
          
            return Page();
        }

    }
}
