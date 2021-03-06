using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Components
{
    public partial class Confirm
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter] public string ConfirmationTitle { get; set; } = "Confirm Delete";
        [Parameter] public string ConfirmationMessage { get; set; } = "Are you sure you want to delete this?";
    

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter] public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}