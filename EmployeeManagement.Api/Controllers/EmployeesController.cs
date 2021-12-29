using EmployeeManagement.Api.Repositories;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            try
            {
                EmployeeDTO result = await _employeeRepository.GetEmployee(id);

                if(result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                if (id != employeeDTO.EmployeeId)
                {
                    return BadRequest("Employee ID mismatch");
                }

                EmployeeDTO employeeToUpdate = await _employeeRepository.GetEmployee(id);

                if(employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await _employeeRepository.UpdateEmployee(employeeDTO);
                // return Ok(employee);
                // return employee;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeDTO == null)
                {
                    return BadRequest();
                }

                EmployeeDTO emp = await _employeeRepository.GetEmployeeByEmail(employeeDTO.Email);
                if (emp != null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                    return BadRequest(ModelState);
                }


                EmployeeDTO createdEmployee = await _employeeRepository.AddEmployee(employeeDTO);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(int id)
        {
            try
            {
                EmployeeDTO employeeToDelete = await _employeeRepository.GetEmployee(id);

                if(employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                 return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }



        
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Search(string name, Gender? gender)
        {
            try
            {
                IEnumerable<EmployeeDTO> result = await _employeeRepository.Search(name, gender);

                if(result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
