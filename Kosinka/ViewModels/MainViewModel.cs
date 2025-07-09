using CommunityToolkit.Mvvm.Input;

using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;
using Kosinka.Helpers;

using MsBox.Avalonia;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kosinka.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public Action<Card?>? NewCardControl { get; set; }
    public required IGetNextCard GetNext {  get; set; }
    public required ObservableCollection<Card> Deck { get; set; } = [];
    public ObservableCollection<Card> AceHeart { get; set; } = [];
    public ObservableCollection<Card> AceDiamond { get; set; } = [];
    public ObservableCollection<Card> AceClub { get; set; } = [];
    public ObservableCollection<Card> AceSpade { get; set; } = [];

    public required ObservableCollection<Card> TableDeck1 { get; set; } = [];
    public required ObservableCollection<Card> TableDeck2 { get; set; } = [];
    public required ObservableCollection<Card> TableDeck3 { get; set; } = [];
    public required ObservableCollection<Card> TableDeck4 { get; set; } = [];
    public required ObservableCollection<Card> TableDeck5 { get; set; } = [];
    public required ObservableCollection<Card> TableDeck6 { get; set; } = [];
    public required ObservableCollection<Card> TableDeck7 { get; set; } = [];

    private Card? _currentCard = null;
    public Card? CurrentCard { get=> _currentCard; set { _currentCard = value; OnPropertyChanged(nameof(CurrentCard)); } }

    [RelayCommand]
    private async Task NextCard()
    {
        try
        {
            CurrentCard = GetNext.NextCard(Deck);
            NewCardControl?.Invoke(CurrentCard);
        }
        catch(Exception e)
        {
            await MessageBoxManager.GetMessageBoxStandard(MessageBoxParamsHelper.GetErrorBox($"Ошибка при попытке получения следующей карты -> {e.Message}")).ShowAsync();
        }
    }
}
