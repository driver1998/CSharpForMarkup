using Xaml = Windows.UI.Xaml;
#if GENERATE
using CSharpMarkup.Generate.WinUI;
using Controls = Windows.UI.Xaml.Controls;

[assembly: MarkupHelpers(
    markupHelpersType: typeof(CSharpMarkup.SystemXaml.Helpers)
)]
#endif

namespace CSharpMarkup.SystemXaml
{
    public static partial class Helpers
    {
        #if GENERATE
        /// <summary>Used by codegen to generate a <see cref="IDefaultBindProperty"/> implementation on markup types. Not used at runtime.</summary>
        /// <remarks>Types must be fully specified for codegen to work</remarks>
        static Xaml.DependencyProperty[] DefaultBindProperties => new Xaml.DependencyProperty[]
        {
            Windows.UI.Xaml.Controls.AutoSuggestBox.TextProperty,
            Windows.UI.Xaml.Controls.BitmapIcon.UriSourceProperty,
            Windows.UI.Xaml.Controls.BitmapIconSource.UriSourceProperty,
            Windows.UI.Xaml.Controls.CalendarDatePicker.DateProperty,
            Windows.UI.Xaml.Controls.CalendarViewDayItem.DateProperty,
            Windows.UI.Xaml.Controls.ColorPicker.ColorProperty,
            Windows.UI.Xaml.Controls.ColumnDefinition.WidthProperty,
            Windows.UI.Xaml.Controls.ContentControl.ContentProperty,
            Windows.UI.Xaml.Controls.ContentPresenter.ContentProperty,
            Windows.UI.Xaml.Controls.DatePicker.DateProperty,
            Windows.UI.Xaml.Controls.DatePickerFlyout.DateProperty,
            Windows.UI.Xaml.Controls.FontIcon.GlyphProperty,
            Windows.UI.Xaml.Controls.FontIconSource.GlyphProperty,
            Windows.UI.Xaml.Controls.Image.SourceProperty,
            Windows.UI.Xaml.Controls.ItemsControl.ItemsSourceProperty,
            Windows.UI.Xaml.Controls.ListPickerFlyout.ItemsSourceProperty,
            Windows.UI.Xaml.Controls.PasswordBox.PasswordProperty,
            Windows.UI.Xaml.Controls.PathIcon.DataProperty,
            Windows.UI.Xaml.Controls.PathIconSource.DataProperty,
            Windows.UI.Xaml.Controls.Primitives.ButtonBase.CommandProperty,
            Windows.UI.Xaml.Controls.Primitives.ColorSpectrum.ColorProperty,
            Windows.UI.Xaml.Controls.Primitives.RangeBase.ValueProperty,
            Windows.UI.Xaml.Controls.Primitives.SelectorItem.IsSelectedProperty,
            Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsCheckedProperty,
            Windows.UI.Xaml.Controls.ProgressRing.IsActiveProperty,
            Windows.UI.Xaml.Controls.RatingControl.ValueProperty,
            Windows.UI.Xaml.Controls.RowDefinition.HeightProperty,
            Windows.UI.Xaml.Controls.SplitButton.CommandProperty,
            Windows.UI.Xaml.Controls.SwipeItem.CommandProperty,
            Windows.UI.Xaml.Controls.SymbolIcon.SymbolProperty,
            Windows.UI.Xaml.Controls.SymbolIconSource.SymbolProperty,
            Windows.UI.Xaml.Controls.TextBlock.TextProperty,
            Windows.UI.Xaml.Controls.TextBox.TextProperty,
            Windows.UI.Xaml.Controls.TimePicker.TimeProperty,
            Windows.UI.Xaml.Controls.TimePickerFlyout.TimeProperty,
            Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty,
            Windows.UI.Xaml.Controls.ToggleSwitch.IsOnProperty,
            Windows.UI.Xaml.Controls.TreeView.ItemsSourceProperty,
            Windows.UI.Xaml.Controls.TreeViewItem.ItemsSourceProperty,
            Windows.UI.Xaml.Controls.TreeViewNode.ContentProperty,

            // TODO: Complete items after last one above + from subnamespaces Documents, Input, Media, Media.Animation, Shapes

            // TODO: Consider supporting UNO-only Reveal* brush types
            
            Windows.UI.Xaml.Media.ImageBrush.ImageSourceProperty,
            Windows.UI.Xaml.Media.SolidColorBrush.ColorProperty,
        };
        #endif
        
        /* TODO: Enable to specify DefaultBindProperties that exist in UNO but not in WinUI, e.g.:
           Windows.UI.Xaml.Controls.CaptureElement.SourceProperty,
           Windows.UI.Xaml.Controls.MediaElement.SourceProperty,
           This could be done by copying the entire array in source within #if HAS_UNO #else
           Not worth it for just two properties so postpone.
        */

        // Helper signatures - codegen will generate the method body based on the parameter names and types
        // Note that for now the signature must be in the _Helpers.cs file

        public static partial TextBlock TextBlock(string Text); // Specify parameter properties

        static partial void Timeline_IncludeInDerived(System.TimeSpan? BeginTime, Windows.UI.Xaml.Duration Duration);
        // Specify parameter properties to include them in derived types

        /// <summary>Create a <see cref="Xaml.Controls.ContentPresenter"/></summary>
        /// <remarks>Remark: ContentPresenter().Bind() binds to <see cref="Xaml.Controls.ContentPresenter.ContentProperty"/></remarks>
        public static ContentPresenter ContentPresenter()
        {
            var ui = new Xaml.Controls.ContentPresenter();
            var markup = CSharpMarkup.SystemXaml.ContentPresenter.StartChain(ui);

            // In WinUI we use a workaround to simulate template bindings, so we have to explicitly create equivalent bindings
            // for the bindings that ContentPresenter creates by default.
            if (DependencyObjectExtensions.TemplatedParent is not null)
                markup.Content().BindTemplate("Content")
                      .ContentTemplate().BindTemplate("ContentTemplate")
                      .ContentTemplateSelector().BindTemplate("ContentTemplateSelector");

            return markup;
        }

        /// <summary>This helper allows to configure an existing UI framework page object instance, i.e. when a page instance is created by the UI framework when specifying the page type in navigation</summary>
        public static Page Content(this Xaml.Controls.Page page, Xaml.UIElement content)
        {
            page.Content = content;
            return page;
        }
    }
}