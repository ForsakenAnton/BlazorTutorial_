using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Web.Services
{
    // Не ставил nuget Microsoft.AspNetCore.Blazor.HttpClient
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private string url = "api/employees";

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDTO>>(url);

            // Ниже шпаргалка, которая тоже работает
            //IEnumerable<Employee> result = null; // = new List<Employee>();
            //HttpResponseMessage response = await _httpClient.GetAsync(url);

            //if (response.IsSuccessStatusCode)
            //{
            //    var stringResponse = await response.Content.ReadAsStringAsync();

            //    result = JsonSerializer.Deserialize<IEnumerable<Employee>>(
            //        json: stringResponse,
            //        options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //}

            //return result;
        }

        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDTO>(url + $"/{id}");
        }

        public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO updatedEmployeeDTO)
        {
            //return await _httpClient.PutAsJsonAsync<Employee>(url, updatedEmployee);
            EmployeeDTO result = null;

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<EmployeeDTO>($"{url}/{updatedEmployeeDTO.EmployeeId}", updatedEmployeeDTO);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<EmployeeDTO>(
                    json: stringResponse,
                    options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            }

            return result;
        }

        public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO newEmployeeDTO)
        {
            EmployeeDTO result = null;

            string json = JsonSerializer.Serialize(newEmployeeDTO, new JsonSerializerOptions() { WriteIndented = true});
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<EmployeeDTO>(url, newEmployeeDTO);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<EmployeeDTO>(
                    json: stringResponse,
                    options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return result;
        }

        public async Task DeleteEmployee(int id)
        {
            await _httpClient.DeleteAsync($"{url}/{id}");
        }
    }
}
