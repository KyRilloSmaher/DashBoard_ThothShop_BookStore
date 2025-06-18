using Microsoft.AspNetCore.Mvc;
using ThothStore_DashBoard.Models.Auth;
using ThothStore_DashBoard.Services.Interfaces;
using ThothStore_DashBoard.Views.Account;

namespace ThothStore_DashBoard.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAPIService _apiService;

        public AccountController(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Home()
        {
            return View();
        }

    }
}
