using System.Linq;
using CSharpMarkup.WinUI;
using static CSharpMarkup.WinUI.Helpers;
namespace WinUICsMarkupExamples;

public partial class FlutterPage
{
    StackPanel Markup => VStack (
        HStack (
            Button ("\u1438") .Style (HeaderButton)
               .Width (50)
               .Bind (vm.BackCommand),

            TextBlock ()
               .VCenter ()
               .Bind (vm.Title)
        ),

        Button () .Template (RoundButton)
            .Foreground (Black) .Background (Gold) .Size (75)
            .Content().Bind (vm.ToggleMoreText)
            .Bind (vm.ToggleMoreCommand),

        vm.ShowMore ?
            TextBlock ("Demonstrate Flutter-like UI building features:\nConditional UI and the Spread() operator")
            : null,

        TextBlock("Subtitles:"),
        Spread (Subtitles),

        TextBlock ("Pairs:"),
        Spread (Pairs())

    )  .Background (Black);

    IEnumerable<UI.UIElement> Subtitles => vm.Subtitles.Select(subtitle => TextBlock(subtitle) .Margin (0, 5) .UI);

    IEnumerable <UI.UIElement> Pairs() {
        for (int i = 1; i <= 5; i++) {
            yield return TextBlock($"Field {i}:") .Margins (top: 20);
            yield return TextBox(PlaceholderText: $"Enter value for {i}");
        }
    }

    ControlTemplate RoundButton = ControlTemplate (typeof(Button), () =>
        Grid (
            Ellipse ()
                .Fill().BindTemplate(b?.Background) 
                .Stroke().BindTemplate(b?.Foreground),
            ContentPresenter ()
                .Center()
        ));
}