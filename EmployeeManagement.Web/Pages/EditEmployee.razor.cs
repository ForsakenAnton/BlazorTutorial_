
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace EmployeeManagement.Web.Pages
{
    public partial class EditEmployee
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject] public IEmployeeService EmployeeService { get; set; }
        public EmployeeDTO EditEmployeeModel { get; set; } = new();

        [Inject] public IDepartmentService DepartmentService { get; set; }
        public List<Department> Departments { get; set; } = new();
        
        [Parameter] public string Id { get; set; }
        public string PageHeader { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/editEmployee/{Id}");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            int.TryParse(Id, out int employeeId);

            if (employeeId != 0)
            {
                PageHeader = "Edit Employee";
                EditEmployeeModel = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeader = "Create Employee";
                EditEmployeeModel = new EmployeeDTO
                {
                    Gender = Gender.Other,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg",
                    //Department = new()
                };
            }

            Departments = (await DepartmentService.GetDepartments()).ToList();
        }

        protected async Task HandleValidSubmit()
        {
            EmployeeDTO result = null;

            if (EditEmployeeModel.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(EditEmployeeModel);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(EditEmployeeModel);
            }

            if(result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(EditEmployeeModel.EmployeeId);
            NavigationManager.NavigateTo("/");
        }
    }
}
