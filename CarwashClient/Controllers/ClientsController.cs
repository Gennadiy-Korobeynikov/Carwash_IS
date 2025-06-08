using CarwashClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
            var clients = await _httpClient.GetFromJsonAsync<List<ClientVM>>(_apiBaseUrl);
            return View(clients);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await _httpClient.GetFromJsonAsync<ClientVM>($"{_apiBaseUrl}/{id}");
            return View(client);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientVM client)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, client);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Ошибка при добавлении клиента.");
            return View(client);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var client = await _httpClient.GetFromJsonAsync<ClientVM>($"{_apiBaseUrl}/{id}");
            return View(client);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientVM client)
        {
            var jsonContent = JsonContent.Create(client);
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Ошибка при редактировании.");
            return View(client);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await _httpClient.GetFromJsonAsync<ClientVM>($"{_apiBaseUrl}/{id}");
            return View(client);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }


    }
}
