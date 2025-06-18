using Microsoft.AspNetCore.Mvc;
using ThithStore_DashBoard.Controllers;
using ThothStore_DashBoard.Models.Bases;
using ThothStore_DashBoard.Models.Enums;
using ThothStore_DashBoard.Models.Order;
using ThothStore_DashBoard.Models.Order.VM;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Services.Interfaces;

namespace ThothStore_DashBoard.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IAPIService _apiService;

        public OrderController(ILogger<OrderController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Home(PagintedRequest AllOrdersRequest)
        {
          

            // Get Order Count
            var getNumberOfOrders = await _apiService.GetAsync<int?>(APIRoutes.GetTotalNumberOfOrders);
            // Get TotalSales
            var getTotalSales = await _apiService.GetAsync<int?>(APIRoutes.GetTotalSales);

            // Get  OrdersList pagineted
            var OrdersListResponse = await _apiService.GetAsyncPaginated<OrderResponse>(
                APIRoutes.GetAllPaginatedOrders.Replace("{PageNumber}", AllOrdersRequest.PageNumber.ToString()).Replace("{PageSize}", AllOrdersRequest.PageSize.ToString())
             );
            OrderPaginetedListResponse? OrdersList = null;
            if (OrdersListResponse.Succeeded) {
                 OrdersList = new OrderPaginetedListResponse {
                    orders = OrdersListResponse.Data,
                    CurrentPage = OrdersListResponse.CurrentPage,
                    TotalCount = OrdersListResponse.TotalCount,
                    TotalPages = OrdersListResponse.TotalPages,
                };
            }
            // get uncompleteorders
            var UncompleteOrdersListResponse = await _apiService.GetAsyncPaginated<OrderResponse>(APIRoutes.GetOrderByStatus.Replace("{status}", OrderStatus.Pending.ToString()));
            IEnumerable<OrderResponse>? UncompleteOrdersList = null;
            if (UncompleteOrdersListResponse.Succeeded)
            {
                UncompleteOrdersList= UncompleteOrdersListResponse.Data;
            }
            // get recentorders
            var RecentOrdersListResponse = await _apiService.GetAsync<IEnumerable<OrderResponse>>(APIRoutes.GetRecentOrders);
            IEnumerable<OrderResponse>? RecentOrdersList = null;
            if (RecentOrdersListResponse.Succeeded)
            {
                RecentOrdersList = RecentOrdersListResponse.Data;
                   
            }

            // Create View Model 
            OrderHomePageVM Vm = new OrderHomePageVM { 
             orderCount = getNumberOfOrders.Data,
             TotalSales = getTotalSales.Data,
             Allorders = OrdersList,
             UnCompleteOrders = UncompleteOrdersList,
             RecentOrders = RecentOrdersList
            };
            return View("Home" , Vm);
        }
    }
}
