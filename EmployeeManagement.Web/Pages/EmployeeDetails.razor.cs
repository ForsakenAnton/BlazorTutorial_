using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EmployeeManagement.Web.Pages
{
    public partial class EmployeeDetails
    {
        public EmployeeDTO EmployeeDTO { get; set; } = new();
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Parameter] public int? Id { get; set; }

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; } = string.Empty;
        protected string Description { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? 1;
            EmployeeDTO = await EmployeeService.GetEmployee(Id.Value);
        }


        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "d-none";
            }
            else
            {
                CssClass = "";
                ButtonText = "Hide Footer";
            }
        }
    }
}
