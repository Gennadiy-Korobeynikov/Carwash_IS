using CarwashClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarwashClient.Controllers
{
    public class ClientsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44316/api/clients";


        public ClientsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _httpClient.GetFromJsonAsync<List<Client>>(_apiBaseUrl);
            return View(clients);
        }
    }
}
