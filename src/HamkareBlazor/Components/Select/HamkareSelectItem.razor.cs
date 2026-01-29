using Microsoft.AspNetCore.Components;
using HamkareBlazor.Extensions;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable
    /// <summary>
    /// A selectable option displayed within a <see cref="HamkareSelect{T}"/> component.
    /// </summary>
    /// <typeparam name="T">The type of value linked to this item.  Must be the same type as the parent <see cref="HamkareSelect{T}"/>.</typeparam>
    /// <seealso cref="HamkareSelect{T}"/>
    public partial class HamkareSelectItem<T> : HamkareComponentBase, IDisposable
    {
        private IHamkareSelect? _parent;
        private IHamkareShadowSelect? _shadowParent;

        private string GetCssClasses() => new CssBuilder()
            .AddClass(Class)
            .Build();

        internal string ItemId { get; } = Identifier.Create();

        /// <summary>
        /// The <see cref="HamkareSelect{T}"/> hosting this item.
        /// </summary>
        [CascadingParameter]
        internal IHamkareSelect? IHamkareSelect
        {
            get => _parent;
            set
            {
                _parent = value;
                if (_parent == null)
                    return;
                _parent.CheckGenericTypeMatch(this);
                if (HamkareSelect == null)
                    return;
                var selected = HamkareSelect.Add(this);
                if (_parent.MultiSelection)
                {
                    HamkareSelect.SelectionChangedFromOutside += OnUpdateSelectionStateFromOutside;
                    InvokeAsync(() => OnUpdateSelectionStateFromOutside(HamkareSelect.GetState(x => x.SelectedValues)));
                }
                else
                {
                    Selected = selected;
                }
            }
        }

        [CascadingParameter]
        internal IHamkareShadowSelect? IHamkareShadowSelect
        {
            get => _shadowParent;
            set
            {
                _shadowParent = value;
                ((HamkareSelect<T>?)_shadowParent)?.RegisterShadowItem(this);
            }
        }

        /// <summary>
        /// Select items with HideContent==true are only there to register their RenderFragment with the select but
        /// wont render and have no other purpose!
        /// </summary>
        [CascadingParameter(Name = "HideContent")]
        internal bool HideContent { get; set; }

        internal HamkareSelect<T>? HamkareSelect => (HamkareSelect<T>?)IHamkareSelect;

        private void OnUpdateSelectionStateFromOutside(IEnumerable<T?>? selection)
        {
            if (selection == null)
                return;
            var oldSelected = Selected;
            Selected = selection.Contains(Value);
            if (oldSelected != Selected)
                InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// The custom value associated with this item.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Behavior)]
        public T? Value { get; set; }

        /// <summary>
        /// Prevents the user from interacting with this item.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.General.Behavior)]
        public bool Disabled { get; set; }

        /// <summary>
        /// The custom content within this item.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.General.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Whether multi-selection is enabled in the parent <see cref="HamkareSelect{T}"/>.
        /// </summary>
        protected bool MultiSelection => HamkareSelect is { MultiSelection: true };

        /// <summary>
        /// Whether this item is selected.
        /// </summary>
        /// <remarks>
        /// Only applies when <see cref="MultiSelection"/> is <c>true</c>.
        /// </remarks>
        internal bool Selected { get; set; }

        /// <summary>
        /// The icon to display whether this item is selected.
        /// </summary>
        /// <remarks>
        /// When <see cref="Selected"/> is <c>true</c>, <see cref="Icons.Material.Filled.CheckBox"/> is returned.  Otherwise, <see cref="Icons.Material.Filled.CheckBoxOutlineBlank"/>.
        /// </remarks>
        protected string? CheckBoxIcon
        {
            get
            {
                if (!MultiSelection)
                    return null;
                return Selected ? Icons.Material.Filled.CheckBox : Icons.Material.Filled.CheckBoxOutlineBlank;
            }
        }

        protected string? DisplayString
        {
            get
            {
                // Use the parent's ConvertValueToString which delegates to ConvertSet (handles ToStringFunc)
                return HamkareSelect?.ConvertValueToString(Value) ?? $"{Value}";
            }
        }

        private async Task OnClickHandleAsync()
        {
            if (MultiSelection)
            {
                Selected = !Selected;
            }

            if (HamkareSelect != null)
                await HamkareSelect.SelectOption(Value);

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Releases resources used by this component.
        /// </summary>
        public void Dispose()
        {
            try
            {
                HamkareSelect?.Remove(this);
                ((HamkareSelect<T>?)_shadowParent)?.UnregisterShadowItem(this);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
