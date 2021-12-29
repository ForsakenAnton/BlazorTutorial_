using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject] public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<EmployeeDTO> EmployeesDTO { get; set; }

        public bool ShowFooter { get; set; } = true;

        protected int SelectedEmoloyeesCount { get; set; } = 0;



        protected override async Task OnInitializedAsync()
        {
            EmployeesDTO = (await EmployeeService.GetEmployees()).ToList();

            //await Task.Run(LoadEmployees);
            //LoadEmployees();
            //return base.OnInitializedAsync();
        }

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmoloyeesCount++;
            }
            else
            {
                SelectedEmoloyeesCount--;
            }
        }

        protected async Task EmployeeDeleted()
        {
            EmployeesDTO = (await EmployeeService.GetEmployees()).ToList();
        }

        //private async void LoadEmployees()
        //{
        //    System.Threading.Thread.Sleep(1000);

        //    Employee e1 = new Employee
        //    {
        //        EmployeeId = 1,
        //        FirstName = "John",
        //        LastName = "Hastings",
        //        Email = "David@pragimtech.com",
        //        DateOfBrith = new DateTime(1980, 10, 5),
        //        Gender = Gender.Male,
        //        //Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
        //        PhotoPath = "images/john.png"
        //    };

        //    Employee e2 = new Employee
        //    {
        //        EmployeeId = 2,
        //        FirstName = "Sam",
        //        LastName = "Galloway",
        //        Email = "Sam@pragimtech.com",
        //        DateOfBrith = new DateTime(1981, 12, 22),
        //        Gender = Gender.Male,
        //        //Department = new Department { DepartmentId = 2, DepartmentName = "HR" },
        //        PhotoPath = "images/sam.jpg"
        //    };

        //    Employee e3 = new Employee
        //    {
        //        EmployeeId = 3,
        //        FirstName = "Mary",
        //        LastName = "Smith",
        //        Email = "mary@pragimtech.com",
        //        DateOfBrith = new DateTime(1979, 11, 11),
        //        Gender = Gender.Female,
        //        //Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
        //        PhotoPath = "images/mary.png"
        //    };

        //    Employee e4 = new Employee
        //    {
        //        EmployeeId = 3,
        //        FirstName = "Sara",
        //        LastName = "Longway",
        //        Email = "sara@pragimtech.com",
        //        DateOfBrith = new DateTime(1982, 9, 23),
        //        Gender = Gender.Female,
        //        //Department = new Department { DepartmentId = 3, DepartmentName = "Payroll" },
        //        PhotoPath = "images/sara.png"
        //    };

        //    Employees = new List<Employee> { e1, e2, e3, e4 };
        //}
    }
}
