using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThothStore_DashBoard.Models.Bases;
using ThothStore_DashBoard.Models.Book.VM;
using ThothStore_DashBoard.Models.Book;
using ThothStore_DashBoard.Models.Category;
using ThothStore_DashBoard.Services.Interfaces;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Models.Author;
using ThothStore_DashBoard.Models.Author.VM;
using ThothStore_DashBoard.Models.Enums;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace ThothStore_DashBoard.Controllers
{
    public class AuthorController : Controller
    {
    
        private readonly ILogger<AuthorController> _logger;
        private readonly IAPIService _apiService;
        public AuthorController(ILogger<AuthorController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Home(PagintedRequest request)
        {
            var AllAuthorsResponse = await _apiService.GetAsyncPaginated<AuthorResponse>(
                 APIRoutes.GetAllPaginatedAuthors.Replace("{PageNumber}", request.PageNumber.ToString()).Replace("{PageSize}", request.PageSize.ToString())
                );
            var TotalNumberOfAuthorsResponse = await _apiService.GetAsync<int>(APIRoutes.GetTotalNumberOfAuthors);
          
            if (AllAuthorsResponse.Succeeded && TotalNumberOfAuthorsResponse.Succeeded)
            {
                AuthorPaginededListResponse AuthorList = new AuthorPaginededListResponse
                {
                    authors = AllAuthorsResponse.Data,
                    CurrentPage = request.PageNumber,
                    TotalCount = TotalNumberOfAuthorsResponse.Data,
                    TotalPages = AllAuthorsResponse.TotalPages,
                
                };
                return View("Home", AuthorList);
            }
            return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string term)
        {

            var SearchResultResponse = await _apiService.GetAsync<IEnumerable<AuthorResponse>>(
                 APIRoutes.SearchForAuthors.Replace("{searchTerm}", term)
                );
            var TotalNumberOfAuthorsResponse = await _apiService.GetAsync<int>(APIRoutes.GetTotalNumberOfAuthors);
            if (SearchResultResponse.Succeeded && TotalNumberOfAuthorsResponse.Succeeded)
            {
                AuthorPaginededListResponse AuthorList = new AuthorPaginededListResponse
                {
                    authors = SearchResultResponse.Data,
                    CurrentPage = -1,
                    TotalCount = TotalNumberOfAuthorsResponse.Data,
                    TotalPages = -1,
                };
                return View("Home", AuthorList);
            }
            return View("Error");

        }
        public async Task<IActionResult> OpenCreateAuthorPage()
        {
            var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(gender=> new SelectListItem { 
              Value = ((int)gender).ToString(),
              Text = gender.ToString(),
            });
            var nationalities = Enum.GetValues(typeof(Nationality)).Cast<Nationality>().Select(gender => new SelectListItem
            {
                Value = ((int)gender).ToString(),
                Text = gender.ToString(),
            });

            ViewBag.Genders = genders;
            ViewBag.Nationalities = nationalities;
                return View("CreateAuthor");

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostAsync<string>(APIRoutes.CreateAuthor, dto, true);

                if (response.Succeeded)
                {
                    return RedirectToAction("Home");
                }
            }
            return View("Error");
        }

        [HttpGet("Author/OpenUpdateAuthorPage/{Id}")]
        public async Task<IActionResult> OpenUpdateAuthorPage([FromRoute] Guid Id)
        {
            var response = await _apiService.GetAsync<AuthorResponse>(
                 APIRoutes.GetAuthorById.Replace("{Id}", Id.ToString())
                );
            var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(gender => new SelectListItem
            {
                Value = ((int)gender).ToString(),
                Text = gender.ToString(),
            });
            var nationalities = Enum.GetValues(typeof(Nationality)).Cast<Nationality>().Select(gender => new SelectListItem
            {
                Value = ((int)gender).ToString(),
                Text = gender.ToString(),
            });

            if (response.Succeeded)
            {

                ViewBag.Genders = genders;
                ViewBag.Nationalities = nationalities;
                ViewBag.author = response.Data;
                return View("UpdateAuthor");
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PutAsync<string>(APIRoutes.UpdateAuthor, dto, true);

                if (response.Succeeded)
                {
                    return RedirectToAction("Home");
                }
            }
            return View("Error");
        }
       
        public async Task<IActionResult> OpenDeletePage([FromRoute] Guid id)
        {
            var response = await _apiService.GetAsync<AuthorResponse>(
                 APIRoutes.GetAuthorById.Replace("{Id}", id.ToString())
                );
            if (response.Succeeded)
            {
                return View("DeleteAuthor",response.Data);
            }
            return RedirectToAction("Error");
        }


        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _apiService.DeleteAsync(
                 APIRoutes.DeleteAuthor.Replace("{Id}", id.ToString())
                );
            if (response)
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Error");
        }
    }
}

