using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Components
{
    public partial class DisplayEmployee
    {
        [Parameter] public EmployeeDTO EmployeeDTO { get; set; }

        [Parameter] public bool ShowFooter { get; set; }

        [Parameter] public EventCallback<bool> OnEmployeeSelection { get; set; }


        [Parameter] public EventCallback<int> OnEmployeeDeleted { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject]public NavigationManager NavigationManager { get; set; }

        protected bool IsSelected { get; set; }



        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnEmployeeSelection.InvokeAsync(IsSelected);
        }


        protected EmployeeManagement.Web.Components.Confirm DeleteConfirmation { get; set; }
        protected void Delete_Click()
        {
            //await EmployeeService.DeleteEmployee(EmployeeDTO.EmployeeId);
            //await OnEmployeeDeleted.InvokeAsync(EmployeeDTO.EmployeeId);
            DeleteConfirmation.Show();
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(EmployeeDTO.EmployeeId);
                await OnEmployeeDeleted.InvokeAsync(EmployeeDTO.EmployeeId);
            }
        }
    }
}
