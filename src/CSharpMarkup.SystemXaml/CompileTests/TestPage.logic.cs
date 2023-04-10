#if DEBUG
using Windows.UI.Xaml.Controls;
using static Windows.UI.Colors;
using Color = Windows.UI.Color;
using Markup = CSharpMarkup.SystemXaml;
using UI = Windows.UI.Xaml;

namespace CompileTests
{
    partial class TestPage : Page
    {
        TestViewModel vm = new();

        Button button;
        readonly Markup.FuncConverter<bool, UI.Media.Brush> okBrushConverter;

        public TestPage()
        {
            okBrushConverter = new (OkBrush);
            // Set viewmodel and set content to markup here
        }

        UI.Media.Brush OkBrush(bool isOk) => new UI.Media.SolidColorBrush(OkColor(isOk));

        Color OkColor(bool isOk) => isOk ? Green : Red;
    }
}
#endif