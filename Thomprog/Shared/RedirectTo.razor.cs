using Microsoft.AspNetCore.Components;

namespace Thomprog.Shared
{
    public partial class RedirectTo
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        public string? To { get; set; }

        protected override void OnInitialized()
        {
            if (!string.IsNullOrWhiteSpace(To))
            {
                NavigationManager.NavigateTo(To);
            }

            base.OnInitialized();
        }

    }
}
