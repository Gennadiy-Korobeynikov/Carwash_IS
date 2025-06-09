using CarwashClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CarwashClient.Controllers
{
    public class ServicesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44316/api/services";


        public ServicesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<IActionResult> Index()
        {
            var services = await _httpClient.GetFromJsonAsync<List<ServiceVM>>(_apiBaseUrl);
            return View(services);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _httpClient.GetFromJsonAsync<ServiceVM>($"{_apiBaseUrl}/{id}");
            return View(service);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceVM service)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, service);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Ошибка при добавлении услуги.");
            return View(service);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var service = await _httpClient.GetFromJsonAsync<ServiceVM>($"{_apiBaseUrl}/{id}");
            return View(service);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceVM service)
        {
            var jsonContent = JsonContent.Create(service);
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Ошибка при редактировании.");
            return View(service);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var service = await _httpClient.GetFromJsonAsync<ServiceVM>($"{_apiBaseUrl}/{id}");
            return View(service);
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
