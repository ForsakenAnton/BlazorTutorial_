using EmployeeManagement.Models;

namespace EmployeeManagement.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        private string url = "api/departments";

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"{url}/{id}");
        }

        public Task<IEnumerable<Department>> GetDepartments()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Department>>(url);
        }
    }
}
