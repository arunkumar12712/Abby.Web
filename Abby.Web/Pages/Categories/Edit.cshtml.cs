using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.Web.Model;
using Abby.Web.Data;

namespace Abby.Web.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);

        }


        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Displayorder cannot exactly match name");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            
            return Page();
        }

    }
}
