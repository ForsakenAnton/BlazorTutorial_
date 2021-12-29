using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployees();
        Task<EmployeeDTO> GetEmployee(int employeeId);
        Task<EmployeeDTO> AddEmployee(EmployeeDTO employee);
        Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employee);
        Task<EmployeeDTO> DeleteEmployee(int employeeId);


        Task<EmployeeDTO> GetEmployeeByEmail(string email);
        Task<IEnumerable<EmployeeDTO>> Search(string name, Gender? gender);
    }
}
