using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployees();
        Task<EmployeeDTO> GetEmployee(int id);
        Task<EmployeeDTO> UpdateEmployee(EmployeeDTO updatedEmployeeDTO);
        Task<EmployeeDTO> CreateEmployee(EmployeeDTO newEmployeeDTO);
        Task DeleteEmployee(int id);
    }
}
