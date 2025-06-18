using Microsoft.AspNetCore.Mvc;
using ThithStore_DashBoard.Controllers;
using ThothStore_DashBoard.Models.Admin;
using ThothStore_DashBoard.Services;
using ThothStore_DashBoard.Services.Interfaces;

namespace ThothStore_DashBoard.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAPIService _apiService;

        public AdminController(ILogger<HomeController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        public async Task<IActionResult> Home()
        {
            var response =await  _apiService.GetAsync<IEnumerable<AdminResponse>>(APIRoutes.getAllAdmins);
            return View(response.Data);
        }
        public IActionResult OpenCreateForm()
        {
            return View("CreateAdmin");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminRequest admin)
        {
            if (ModelState.IsValid)
            {
                await _apiService.PostAsync<string>(APIRoutes.registerAdmin, admin);
                return RedirectToAction("Home");
            }

            return View(admin);
        }
        [HttpGet("Admin/OpenUpdateForm/{Id}")]
        public async Task<IActionResult> OpenUpdateForm([FromRoute] Guid Id)
        {
            var response = await _apiService.GetAsync<AdminResponse>(
                APIRoutes.getUserById.Replace("{Id}", Id.ToString())
            );

            var admin = response.Data;
            return View("UpdateAdmin", admin);
        }


        [HttpPost("Admin/OpenUpdateForm/{Id}")]
        public async Task<IActionResult> Update(AdminResponse adminDto)
        {
            if (ModelState.IsValid)
            {
                var reponse =await _apiService.PutAsync<string>(APIRoutes.updateUser, adminDto);
                return RedirectToAction("Home");
            }
            return View("UpdateAdmin",adminDto);
        }
        [HttpGet("Admin/SendResetPasswordCode")]
        public async Task<IActionResult> SendResetPasswordCode([FromQuery]string email)
        {
            var response = await _apiService.PostAsync<string>(APIRoutes.SendResetCode, new { Email = email});
            if (response.StatusCode ==System.Net.HttpStatusCode.OK)
            {
               return RedirectToAction("ConfirmResetPasswordCodePage");
            }
            return RedirectToAction("Home");
        }
        [HttpGet("Admin/ConfirmResetPasswordCodePage")]
        public IActionResult ConfirmResetPasswordCodePage([FromQuery]string email)
        {
            ViewBag.Email = email;
            return View("confirmRestPasswordCode");
        }

        public IActionResult ResetPasswordPage()
        {
            return View("ResetPasswordPage");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmResetPasswordCode(ConfirmReseetPasswordCodeRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostAsync<string>(APIRoutes.ConfirmResetPasswordCode, dto);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("ResetPasswordPage");
                }
            }
            return RedirectToAction("ConfirmResetPasswordCode", dto);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostAsync<string>(APIRoutes.Resetpassword, dto);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   return RedirectToAction("Home");
                }
            }
            return RedirectToAction("ConfirmResetPassword", dto);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _apiService.DeleteAsync(APIRoutes.getUserById.Replace("{Id}", id.ToString()));
            return RedirectToAction("Home");
        }
    }
}
