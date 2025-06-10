
using CarwashClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace CarwashClient.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44316/api/appointments";
        private readonly string _apiBaseBaseUrl = "https://localhost:44316/api/";


        public AppointmentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<IActionResult> Index()
        {
            var appointments = await _httpClient.GetFromJsonAsync<List<AppointmentVM>>(_apiBaseUrl);
            return View(appointments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _httpClient.GetFromJsonAsync<AppointmentVM>($"{_apiBaseUrl}/{id}");
           
            return View(appointment);
        }



        public async Task<IActionResult> Create()
        {
            var vm = new AppointmentFormVM
            {
                Clients = await LoadSelectList("clients"),
                Employees = await LoadSelectList("employees"),
                Statuses = await LoadSelectList("statuses"),
                Spots = await LoadSelectList("spots"),
                Services = await LoadSelectList("services")
            };
            vm.Appointment.DateTime = DateTime.Now.Date;


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                // повторно загрузим списки
                vm.Clients = await LoadSelectList("clients");
                vm.Employees = await LoadSelectList("employees");
                vm.Statuses = await LoadSelectList("statuses");
                vm.Spots = await LoadSelectList("spots");
                vm.Services = await LoadSelectList("services");
                return View(vm);
            }
             //vm.Appointment.Cost = 0;
            var content = new StringContent(
                JsonSerializer.Serialize(vm.Appointment),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                vm.Clients = await LoadSelectList("clients");
                vm.Employees = await LoadSelectList("employees");
                vm.Statuses = await LoadSelectList("statuses");
                vm.Spots = await LoadSelectList("spots");
                vm.Services = await LoadSelectList("services");

                ModelState.AddModelError("", "Это место на это время уже занято.");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        private async Task<List<SelectListItem>> LoadSelectList(string apiUrl)
        {
            var response = await _httpClient.GetAsync(_apiBaseBaseUrl + apiUrl);
            if (!response.IsSuccessStatusCode) return new List<SelectListItem>();

            var json = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<IdNameDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return items!
                .Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name })
                .ToList();
        }



        private class IdNameDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }


        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _httpClient.GetFromJsonAsync<AppointmentCreateDto>($"{_apiBaseUrl}/{id}");

            if (appointment == null)
                return NotFound();

            var vm = new AppointmentFormVM
            {
                Appointment = appointment,
                Clients = await LoadSelectList("clients"),
                Employees = await LoadSelectList("employees"),
                Statuses = await LoadSelectList("statuses"),
                Spots = await LoadSelectList("spots"),
                Services = await LoadSelectList("services")
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentFormVM vm)
        {

            if (!ModelState.IsValid)
            {
                // повторно загрузим списки
                vm.Clients = await LoadSelectList("clients");
                vm.Employees = await LoadSelectList("employees");
                vm.Statuses = await LoadSelectList("statuses");
                vm.Spots = await LoadSelectList("spots");
                vm.Services = await LoadSelectList("services");
                return View(vm);
            }

            var content = new StringContent(
                JsonSerializer.Serialize(vm.Appointment),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                vm.Clients = await LoadSelectList("clients");
                vm.Employees = await LoadSelectList("employees");
                vm.Statuses = await LoadSelectList("statuses");
                vm.Spots = await LoadSelectList("spots");
                vm.Services = await LoadSelectList("services");

                ModelState.AddModelError("", "Ошибка при отправке данных на сервер.");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _httpClient.GetFromJsonAsync<AppointmentVM>($"{_apiBaseUrl}/{id}");
            return View(appointment);
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
