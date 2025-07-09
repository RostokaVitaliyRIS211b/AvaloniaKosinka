using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml.MarkupExtensions;

using Kosinka.Core.Model;
using Kosinka.CustomComponents;
using Kosinka.ViewModels;

using System;

namespace Kosinka.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

    }

    private CardControl? cardControl = null;

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.NewCardControl = NewCardControl;
        }
    }

    private void NewCardControl(Card? card)
    {
        if (DataContext is MainViewModel vm && card is not null)
        {
            if (cardControl is null)
            {
                cardControl = new CardControl() { MyCard = card, MyDeck = vm.Deck, MainCanvas = this.MainCanvas };
                MainCanvas.Children.Add(cardControl);
                Canvas.SetTop(cardControl, 30);
                Canvas.SetLeft(cardControl, 150);
            }
            else if (cardControl is not null)
            {
                if (cardControl.MyDeck == vm.Deck)
                {
                    MainCanvas.Children.Remove(cardControl);
                }

                cardControl = new CardControl() { MyCard = card, MyDeck = vm.Deck, MainCanvas = this.MainCanvas };
                MainCanvas.Children.Add(cardControl);
                Canvas.SetTop(cardControl, 30);
                Canvas.SetLeft(cardControl, 150);
            }
        }
    }
}
