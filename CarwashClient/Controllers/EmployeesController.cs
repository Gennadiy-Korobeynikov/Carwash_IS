using CarwashClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CarwashClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44316/api/employees";


        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<IActionResult> Index()
        {
            var employees = await _httpClient.GetFromJsonAsync<List<EmployeeVM>>(_apiBaseUrl);
            return View(employees);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<EmployeeVM>($"{_apiBaseUrl}/{id}");
            return View(employee);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM employee)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, employee);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Ошибка при добавлении клиента.");
            return View(employee);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<EmployeeVM>($"{_apiBaseUrl}/{id}");
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeVM employee)
        {
            var jsonContent = JsonContent.Create(employee);
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Ошибка при редактировании.");
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<EmployeeVM>($"{_apiBaseUrl}/{id}");
            return View(employee);
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
