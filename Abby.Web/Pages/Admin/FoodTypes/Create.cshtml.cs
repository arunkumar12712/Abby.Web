using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Data;
using Abby.Models;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public FoodType FoodType { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
           
            if (ModelState.IsValid)
            {
                await _db.FoodType.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "FoodType create successfully";
                return RedirectToPage("Index");
            }
            
            return Page();
        }

    }
}
