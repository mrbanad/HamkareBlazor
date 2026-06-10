using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
    /// <summary>
    /// A container which manages <see cref="HamkareExpansionPanel"/> components such that when one panel is expanded the others are collapsed automatically.
    /// </summary>
    /// <seealso cref="HamkareExpansionPanel"/>
    /// <seealso cref="HamkareCollapse"/>
    public partial class HamkareExpansionPanels : HamkareComponentBase
    {
        private readonly List<HamkareExpansionPanel> _panels = new();

        protected string Classname =>
            new CssBuilder("hamkare-expansion-panels")
                .AddClass("hamkare-expansion-panels-square", Square)
                .AddClass(Class)
                .Build();

        /// <summary>
        /// Uses square corners for the panel.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool Square { get; set; }

        /// <summary>
        /// Allows multiple panels to be expanded at the same time.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public bool MultiExpansion { get; set; }

        /// <summary>
        /// The size of the drop shadow.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>1</c>.  A higher number creates a heavier drop shadow.  Use a value of <c>0</c> for no shadow.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public int Elevation { set; get; } = 1;

        /// <summary>
        /// Uses compact padding for all panels.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool Dense { get; set; }

        /// <summary>
        /// Adds left and right padding to all panels.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool Gutters { get; set; } = true;

        /// <summary>
        /// Shows borders around each panel.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Appearance)]
        public bool Outlined { get; set; } = true;

        /// <summary>
        /// The content within this component.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.ExpansionPanel.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// A read-only list of the panels within this component. 
        /// </summary>
        /// <remarks>
        /// Expansion panels are controlled by adding more <see cref="HamkareExpansionPanel"/> components in the Razor page.
        /// </remarks>
        public IReadOnlyList<HamkareExpansionPanel> Panels => _panels;

        internal async Task AddPanelAsync(HamkareExpansionPanel panel)
        {
            if (!MultiExpansion && _panels.Any(p => p._expandedState.Value))
            {
                await panel.CollapseAsync();
            }

            _panels.Add(panel);
        }

        internal void RemovePanel(HamkareExpansionPanel panel)
        {
            _panels.Remove(panel);
            try
            {
                StateHasChanged();
            }
            catch (InvalidOperationException) { /* this happens on page reload, probably a Blazor bug */ }
        }

        internal async Task NotifyPanelsChanged(HamkareExpansionPanel panel)
        {
            if (!MultiExpansion && panel._expandedState.Value)
            {
                await CollapseAllExceptAsync(panel);
                return;
            }

            await UpdateAllAsync();
        }

        /// <summary>
        /// Refreshes the expansion state of all panels.
        /// </summary>
        public Task UpdateAllAsync()
        {
            HamkareExpansionPanel? last = null;
            foreach (var panel in _panels)
            {
                if (last is not null)
                {
                    last.NextPanelExpanded = panel._expandedState.Value;
                }

                last = panel;
            }
            StateHasChanged();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Collapses all panels except the given one.
        /// </summary>
        /// <param name="panel">The panel to keep expanded.</param>
        public async Task CollapseAllExceptAsync(HamkareExpansionPanel panel)
        {
            foreach (var expansionPanel in _panels.Where(expansionPanel => expansionPanel != panel))
            {
                await expansionPanel.CollapseAsync();
            }

            await InvokeAsync(UpdateAllAsync);
        }

        /// <summary>
        /// Hides the content of all panels.
        /// </summary>
        public async Task CollapseAllAsync()
        {
            foreach (var expansionPanel in _panels)
            {
                await expansionPanel.CollapseAsync();
            }
            await InvokeAsync(UpdateAllAsync);
        }

        /// <summary>
        /// Shows the content of all panels.
        /// </summary>
        public async Task ExpandAllAsync()
        {
            foreach (var expansionPanel in _panels)
            {
                await expansionPanel.ExpandAsync();
            }
            await InvokeAsync(UpdateAllAsync);
        }
    }
}
