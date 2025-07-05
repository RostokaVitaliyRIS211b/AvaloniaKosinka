using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

using Kosinka.Core.Model;

using System;
using System.Collections.ObjectModel;

namespace Kosinka.CustomComponents;

public partial class TableDeck : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<Card>> CardsProperty = AvaloniaProperty.Register<TableDeck, ObservableCollection<Card>>(nameof(Cards));
    public static readonly StyledProperty<Canvas> MainCanvasProperty = AvaloniaProperty.Register<CardControl, Canvas>(nameof(MainCanvas));
    public ObservableCollection<Card> Cards
    {
        get => GetValue(CardsProperty);
        set => SetValue(CardsProperty, value);
    }

    public Canvas MainCanvas
    {
        get => GetValue(MainCanvasProperty);
        set => SetValue(MainCanvasProperty, value);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        Initialize();
    }


    private void Initialize()
    {
        double MyLeft = Canvas.GetLeft(this);
        double MyTop = Canvas.GetTop(this);
        int counter = 0;
        foreach (Card card in Cards)
        {
            CardControl cardControl = new() { MyCard = card, MainCanvas = this.MainCanvas };
            MainCanvas.Children.Add(cardControl);
            Canvas.SetLeft(cardControl, MyLeft);
            Canvas.SetTop(cardControl, MyTop + 50 *counter);
            ++counter;
        }
    }
}