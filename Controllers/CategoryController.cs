using Microsoft.AspNetCore.Mvc;
using ThithStore_DashBoard.Controllers;
using ThothStore_DashBoard.Models.Admin;
using ThothStore_DashBoard.Models.Bases;
using ThothStore_DashBoard.Models.Category;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Services.Interfaces;

namespace ThothStore_DashBoard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAPIService _apiService;
        public CategoryController(ILogger<HomeController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Home()
        {
            var response = await _apiService.GetAsync<IEnumerable<CategoryResposnse>>(APIRoutes.getAllCategories);
           
            if (response.Succeeded)
            {
                var CategoryList = response.Data;
                ViewBag.Count = CategoryList.Count();
                return View("Home", CategoryList);
            }
            return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string term)
        {

            var response = await _apiService.GetAsync<CategoryResposnse>(
                 APIRoutes.getCategoryByName.Replace("{term}", term)
                );
            var response2 = await _apiService.GetAsync<IEnumerable<CategoryResposnse>>(APIRoutes.getAllCategories);

            if (response.Succeeded && response2.Succeeded)
            {
                var totalcount = response2.Data.Count();
                ViewBag.Count = totalcount;
                var Category = response.Data;
                if (Category is not null)
                {
                    IEnumerable<CategoryResposnse> CategoryList = [Category];
                    return View("Home", CategoryList);
                }
                else
                {
                    IEnumerable<CategoryResposnse> EmptyCategoryList = new List<CategoryResposnse>();
                    return View("Home", EmptyCategoryList);
                }

            }
            return View("Error");

        }
        public IActionResult OpenCreateCategoryPage() { 
           return View("CreateCategoryPage");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostAsync<string>(APIRoutes.createCategory, dto,true);
              
                if (response.Succeeded)
                {
                    return RedirectToAction("Home");
                }
            }
            return View("Error");
        }
        [HttpGet("Category/OpenUpdateCategoryPage/{Id}")]
        public async Task<IActionResult> OpenUpdateCategoryPage([FromRoute] Guid Id)
        {
            var response = await _apiService.GetAsync<CategoryResposnse>(
              APIRoutes.getCategoryById.Replace("{Id}", Id.ToString())
             );

            if (response.Succeeded)
            {
                var category = response.Data;
                return View("UpdateCategoryPage", category);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PutAsync<string>(APIRoutes.updateCategory, dto,true);

                if (response.Succeeded)
                {
                    return RedirectToAction("Home");
                }
            }
            return View("Error");
        }
        [HttpPost("/Category/Delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response = await _apiService.DeleteAsync(APIRoutes.deleteCategoryById.Replace("{Id}",id.ToString()));
            if (response)
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Error");
        }
    }
}
