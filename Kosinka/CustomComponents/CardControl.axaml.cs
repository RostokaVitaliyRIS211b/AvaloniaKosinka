using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;

using Kosinka.Core.Model;

namespace Kosinka.CustomComponents;

public partial class CardControl : TemplatedControl
{
    public static readonly StyledProperty<Card> CardProperty = AvaloniaProperty.Register<CardControl, Card>(nameof(Card));
    public static readonly StyledProperty<Canvas> MainCanvasProperty = AvaloniaProperty.Register<CardControl, Canvas>(nameof(MainCanvas));
    public Card Card
    {
        get => GetValue(CardProperty);
        set => SetValue(CardProperty, value);
    }

    public Canvas MainCanvas
    {
        get => GetValue(MainCanvasProperty);
        set => SetValue(MainCanvasProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

    }
}