using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThithStore_DashBoard.Controllers;
using ThothStore_DashBoard.Models.Bases;
using ThothStore_DashBoard.Models.Book;
using ThothStore_DashBoard.Models.Book.VM;
using ThothStore_DashBoard.Models.Category;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ThothStore_DashBoard.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IAPIService _apiService;
        public BookController(ILogger<BookController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Home(PagintedRequest request)
        {
            var AllBooksResponse = await _apiService.GetAsyncPaginated<BookResponse>(
                 APIRoutes.GetallPaginatedBooks.Replace("{PageNumber}", request.PageNumber.ToString()).Replace("{PageSize}", request.PageSize.ToString())
                );
            var TotalNumberOfBooksResponse = await _apiService.GetAsync<int>(APIRoutes.GetTotalNumberOfBooks);
            var OutOfStockfBooksResponse = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetOutOfStockBooks);
            var popularBooksResponse = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetTopSellingBooks);
            if (AllBooksResponse.Succeeded && TotalNumberOfBooksResponse.Succeeded)
            {
                BookPaginetedListResponse BookList = new BookPaginetedListResponse
                {
                    books = AllBooksResponse.Data,
                    CurrentPage = request.PageNumber,
                    TotalCount = TotalNumberOfBooksResponse.Data,
                    TotalPages = AllBooksResponse.TotalPages,
                    OutofStockBooks = OutOfStockfBooksResponse.Data,
                    PopularBooks = popularBooksResponse.Data
                };
                return View("Home", BookList);
            }
            return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string term)
        {

            var SearchResultResponse = await _apiService.GetAsync<IEnumerable<BookResponse>>(
                 APIRoutes.SearchForBooks.Replace("{searchTerm}", term)
                );
            var TotalNumberOfBooksResponse = await _apiService.GetAsync<int>(APIRoutes.GetTotalNumberOfBooks);
            var OutOfStockfBooksResponse = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetOutOfStockBooks);
            var popularBooksResponse = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetTopSellingBooks);
            if (SearchResultResponse.Succeeded && TotalNumberOfBooksResponse.Succeeded)
            {
                BookPaginetedListResponse BookList = new BookPaginetedListResponse
                {
                    books = SearchResultResponse.Data,
                    CurrentPage = -1,
                    TotalCount = TotalNumberOfBooksResponse.Data,
                    TotalPages = -1,
                    OutofStockBooks = OutOfStockfBooksResponse.Data,
                    PopularBooks = popularBooksResponse.Data
                };
                return View("Home", BookList);
            }
            return View("Error");

        }
        public async Task<IActionResult> OpenCreateBookPage()
        {
            var getAllCategoriesResponse =  await _apiService.GetAsync<IEnumerable<CategoryResposnse>>(APIRoutes.getAllCategories);
            if (getAllCategoriesResponse.Succeeded)
            {
                ViewBag.Categories = getAllCategoriesResponse.Data.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View("CreateBookPage");
            }
            else
                return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostAsync<string>(APIRoutes.CreateBook, dto, true);

                if (response.Succeeded)
                {
                    return RedirectToAction("Home");
                }
            }
            return View("Error");
        }
        [HttpGet("Book/OpenUpdateBookPage/{Id}")]
        public async Task<IActionResult> OpenUpdateBookPage([FromRoute] Guid Id)
        {
            var response = await _apiService.GetAsync<BookResponse>(
                 APIRoutes.GetBookById.Replace("{Id}", Id.ToString())
                );
            var getAllCategoriesResponse = await _apiService.GetAsync<IEnumerable<CategoryResposnse>>(APIRoutes.getAllCategories);
            if (response.Succeeded && getAllCategoriesResponse.Succeeded)
            {
                ViewBag.Categories = getAllCategoriesResponse.Data.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                var book = response.Data;
                ViewBag.Book = book;
                return View("UpdateBookPage");
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PutAsync<string>(APIRoutes.UpdateAnExistingBook, dto, true);

                if (response.Succeeded)
                {
                    return RedirectToAction("Home");
                }
            }
            return View("Error");
        }
        [HttpPost("/Book/Delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response = await _apiService.DeleteAsync(
                 APIRoutes.DeleteBook.Replace("{Id}", id.ToString())
                );
            if (response)
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Error");
        }
    }
}
