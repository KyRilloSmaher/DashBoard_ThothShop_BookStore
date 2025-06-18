using Microsoft.AspNetCore.Mvc;
using ThothStore_DashBoard.Models.Author;
using ThothStore_DashBoard.Models.Book;
using ThothStore_DashBoard.Models.Order;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Services.Interfaces;

namespace ThothStore_DashBoard.Controllers
{
    public class AnalyticController : Controller
    {
        private readonly ILogger<AnalyticController> _logger;
        private readonly IAPIService _apiService;

        public AnalyticController(ILogger<AnalyticController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Home()
        {
            try
            {
                // Get basic statistics
                var getBookCountResponse = await _apiService.GetAsync<int>(APIRoutes.GetTotalNumberOfBooks);
                var getAuthorCount = await _apiService.GetAsync<int?>(APIRoutes.GetTotalNumberOfAuthors);
                var getTotalSales = await _apiService.GetAsync<int?>(APIRoutes.GetTotalSales);
                var getNumberOfOrders = await _apiService.GetAsync<int?>(APIRoutes.GetTotalNumberOfOrders);

                // Get analytics data
                var recentOrders = await _apiService.GetAsync<IEnumerable<OrderResponse>>(APIRoutes.GetRecentOrders);
                var topAuthors = await _apiService.GetAsync<IEnumerable<AuthorResponse>>(
                    APIRoutes.GetMostPopularAuthors.Replace("{PageNumber}", "1").Replace("{PageSize}", "10")
                );
                var topSellingBooks = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetTopSellingBooks);
                var outOfStockBooks = await _apiService.GetAsync<IEnumerable<BookResponse>>(APIRoutes.GetOutOfStockBooks);

                // Calculate additional analytics
                var averageOrderValue = getNumberOfOrders.Data > 0 ? getTotalSales.Data / getNumberOfOrders.Data : 0;
                var booksPerAuthor = getAuthorCount.Data > 0 ? (double)getBookCountResponse.Data / getAuthorCount.Data : 0;

                // Set ViewBag data
                ViewBag.TotalNumberOfBooks = getBookCountResponse.Data;
                ViewBag.AuthorCount = getAuthorCount.Data;
                ViewBag.TotalSales = getTotalSales.Data;
                ViewBag.NumberOfOrders = getNumberOfOrders.Data;
                ViewBag.AverageOrderValue = averageOrderValue;
                ViewBag.BooksPerAuthor = Math.Round((decimal)booksPerAuthor, 2);
                ViewBag.RecentOrders = recentOrders.Data;
                ViewBag.TopAuthors = topAuthors.Data;
                ViewBag.TopSellingBooks = topSellingBooks.Data;
                ViewBag.OutOfStockBooks = outOfStockBooks.Data;

                return View("Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading analytics data");
                ViewBag.Error = "Failed to load analytics data. Please try again.";
                return View("Home");
            }
        }
    }
}
