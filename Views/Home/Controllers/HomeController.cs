using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using NimbusBank.Frontend.Models;

namespace NimbusBank.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public IActionResult Index() => View();

        public IActionResult Apply() => View();

        [HttpPost]
        public async Task<IActionResult> Apply(LoanApplication model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://nimbus-bank-be.azurewebsites.net/api/loan", content);

            ViewBag.Message = response.IsSuccessStatusCode ? "Success!" : "Error!";
            return View();
        }
    }
}