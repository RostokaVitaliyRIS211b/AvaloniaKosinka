using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using Kosinka.Attributes;
using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;
using Kosinka.CustomComponents;
using Kosinka.ViewModels;
using Kosinka.Views;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Kosinka;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);
        IViewModelBuilder viewModelBuilder = ServiceHelper.ServiceProvider.GetRequiredService<IViewModelBuilder>();
        IGetNextCard getNextCard = ServiceHelper.ServiceProvider.GetRequiredService<IGetNextCard>();
        List<ObservableCollection<Card>> Cards = [.. viewModelBuilder.GetDecks()];

        ObservableCollection<Card> Deck = Cards[^1];

        ObservableCollection<Card> TableDeck1 = Cards[0];
        ObservableCollection<Card> TableDeck2 = Cards[1];
        ObservableCollection<Card> TableDeck3 = Cards[2];
        ObservableCollection<Card> TableDeck4 = Cards[3];
        ObservableCollection<Card> TableDeck5 = Cards[4];
        ObservableCollection<Card> TableDeck6 = Cards[5];
        ObservableCollection<Card> TableDeck7 = Cards[6];

        var context = new MainViewModel()
        {
            GetNext = getNextCard,
            Deck = Deck,
            TableDeck1 = TableDeck1,
            TableDeck2 = TableDeck2,
            TableDeck3 = TableDeck3,
            TableDeck4 = TableDeck4,
            TableDeck5 = TableDeck5,
            TableDeck6 = TableDeck6,
            TableDeck7 = TableDeck7
        };

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = context
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = context
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

}
