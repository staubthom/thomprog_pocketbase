using Microsoft.AspNetCore.Components;

namespace Thomprog.Shared
{
    public partial class Loading
    {
        [Parameter]
        public bool? Visible { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

    }
}
