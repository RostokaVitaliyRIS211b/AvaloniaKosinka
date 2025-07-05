using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

using Kosinka.Core.Model;

using System;
using System.Collections.ObjectModel;

namespace Kosinka.CustomComponents;

public partial class TableDeck : TemplatedControl
{
    private Canvas canvas;
    public static readonly StyledProperty<ObservableCollection<Card>> CardsProperty = AvaloniaProperty.Register<TableDeck, ObservableCollection<Card>>(nameof(Cards));
    public static readonly StyledProperty<Canvas> MainCanvasProperty = AvaloniaProperty.Register<CardControl, Canvas>(nameof(MainCanvas));
    public ObservableCollection<Card> Cards
    {
        get => GetValue(CardsProperty);
        set => SetValue(CardsProperty,value);
    }

    public Canvas MainCanvas
    {
        get => GetValue(MainCanvasProperty);
        set => SetValue(MainCanvasProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        canvas = e.NameScope.Find<Canvas>("PART_canvas") ?? throw new ArgumentNullException("Не найден Canvas");

        Initialize();
    }

    private void Initialize()
    {
        
        
    }
}