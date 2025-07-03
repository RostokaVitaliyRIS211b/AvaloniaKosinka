using Kosinka.Core.Model;

using System.Collections.ObjectModel;

namespace Kosinka.ViewModels;

public partial class MainViewModel : ViewModelBase
{
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
}
