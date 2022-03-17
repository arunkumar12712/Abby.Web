using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Data;
using Abby.Models;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public FoodType FoodType { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public void OnGet(int id)
        {
            FoodType = _db.FoodType.Find(id);

        }


        public async Task<IActionResult> OnPost()
        {
           
            if (ModelState.IsValid)
            {
                _db.FoodType.Update(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Type edit successfully";
                return RedirectToPage("Index");
            }
            
            return Page();
        }

    }
}
