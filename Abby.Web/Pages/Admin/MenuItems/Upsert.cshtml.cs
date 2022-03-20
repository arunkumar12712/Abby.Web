using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Data;
using Abby.Models;
using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abby.Web.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

            MenuItem = new();
        }

       
        public void OnGet(int? id)
        {
            if(id != null)
            {
                // edit
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u=>u.Id == id);
            }

            CategoryList = _unitOfWork.Category.GetAll().Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()  
            });
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString()
            });
        }


        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(MenuItem.Id == 0)
            {
                //create
                string filename_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuitems");
                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, filename_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }; 

                MenuItem.Image = @"\images\menuitems\" +filename_new +extension;
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
                if(files.Count > 0)
                {
                    string filename_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems");
                    var extension = Path.GetExtension(files[0].FileName);

                    // delete the old image

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, filename_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    };
                    MenuItem.Image = @"\images\menuitems\" + filename_new + extension;

                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }
                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
              

            }

            
            
            return RedirectToPage("./Index");
        }

    }
}
