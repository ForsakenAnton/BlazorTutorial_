using AutoMapper;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeManagement.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            //return await _context.Employees.ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(await _context.Employees.ToListAsync());
        }

        public async Task<EmployeeDTO> GetEmployee(int employeeId)
        {
            return _mapper.Map<EmployeeDTO>(
                await _context.Employees
                        .Include(e => e.Department)
                        .FirstOrDefaultAsync(e => e.EmployeeId == employeeId));
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            Employee employeeToAdd = _mapper.Map<Employee>(employeeDTO);
            EntityEntry<Employee> result = await _context.Employees.AddAsync(employeeToAdd);
            await _context.SaveChangesAsync();

            return  _mapper.Map<EmployeeDTO>(result.Entity);
        }

        public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO)
        {
            Employee result = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeDTO.EmployeeId);

            if(result != null)
            {
                result.FirstName = employeeDTO.FirstName;
                result.LastName = employeeDTO.LastName;
                result.Email = employeeDTO.Email;
                result.DateOfBrith = employeeDTO.DateOfBrith;
                result.Gender = employeeDTO.Gender;
                result.DepartmentId = employeeDTO.DepartmentId;
                result.PhotoPath = employeeDTO.PhotoPath;

                await _context.SaveChangesAsync();

                return _mapper.Map<EmployeeDTO>(result);
            }

            return null;
        }

        public async Task<EmployeeDTO> DeleteEmployee(int employeeId)
        {
            Employee result = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if( result != null)
            {
                _context.Employees.Remove(result);
                await _context.SaveChangesAsync();

                return _mapper.Map<EmployeeDTO>(result);
            }

            return null;
        } 



        public async Task<EmployeeDTO> GetEmployeeByEmail(string email)
        {
            var result =  await _context.Employees
                .FirstOrDefaultAsync(e =>e.Email == email);

            return _mapper.Map<EmployeeDTO>(result);
        }

        public async Task<IEnumerable<EmployeeDTO>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name)
                     || e.LastName.Contains(name));
            }

            if(gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return _mapper.Map<IEnumerable<EmployeeDTO>>(await query.ToListAsync());
        }
    }
}
