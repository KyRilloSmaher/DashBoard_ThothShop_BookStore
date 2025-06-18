using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThithStore_DashBoard.Models;
using ThothStore_DashBoard.Models.Auth;
using ThothStore_DashBoard.Models.Author;
using ThothStore_DashBoard.Models.Book;
using ThothStore_DashBoard.Models.Order;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Services.Interfaces;

namespace ThithStore_DashBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAPIService _apiService;

        public HomeController(ILogger<HomeController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _apiService.PostAsync<LoginResponse>(APIRoutes.Login, request);
                if (response.Succeeded)
                {
                    var accessToken = response.Data.AccessToken;
                    var refreshToken = response.Data.RefreshToken.TokenString;
                    var expiresAt = response.Data.RefreshToken.ExpireAt;
                    HttpContext.Session.SetString("AccessToken", accessToken);
                }

                return RedirectToAction("Home", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Login failed. Check your credentials.";
                return View(request);
            }
        }

        public async Task<IActionResult> Home()
        {

            var getBookCountResponse = await _apiService.GetAsync<int>(APIRoutes.GetTotalNumberOfBooks);
            var getAuthorCount = await _apiService.GetAsync<int?>(APIRoutes.GetTotalNumberOfAuthors);
            var getTotalSales = await _apiService.GetAsync<int?>(APIRoutes.GetTotalSales);
            var getNumberOfOrders= await _apiService.GetAsync<int?>(APIRoutes.GetTotalNumberOfOrders);
            var recentOrder = await _apiService.GetAsync<IEnumerable<OrderResponse>>(APIRoutes.GetRecentOrders);
            var TopAuthors = await _apiService.GetAsync<IEnumerable<AuthorResponse>>(
                 APIRoutes.GetMostPopularAuthors.Replace("{PageNumber}", "1").Replace("{PageSize}", "5")
                );
            var TopSellngBooks = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetTopSellingBooks);
            ViewBag.TotalNumberOfBooks = getBookCountResponse.Data;
            ViewBag.AuthorCount = getAuthorCount.Data;
            ViewBag.TotalSales = getTotalSales.Data;
            ViewBag.NumberOfOrders = getNumberOfOrders.Data;
            ViewBag.recentorders = recentOrder.Data;
            ViewBag.TopAuthors = TopAuthors.Data;
            ViewBag.TopSellngBooks = TopSellngBooks.Data;

            return View("Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
