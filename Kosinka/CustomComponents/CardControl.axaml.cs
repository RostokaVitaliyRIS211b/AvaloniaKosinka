using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

using Kosinka.Converters;
using Kosinka.Core.Model;
using Kosinka.Helpers;
using Kosinka.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Kosinka.CustomComponents;


public partial class CardControl : TemplatedControl
{
    protected Point OriginalPosition;
    protected Point PreviuosPosition;
    protected List<CardControl> Train;
    protected int originalZindex;
    public ObservableCollection<Card> MyDeck;
    public bool IsInArea { get; set; } = false;
    public bool IsDragging { get; set; } = false;
    protected Image _image;

    public static readonly StyledProperty<Card> MyCardProperty = AvaloniaProperty.Register<CardControl, Card>(nameof(MyCard));
    public static readonly StyledProperty<Canvas> MainCanvasProperty = AvaloniaProperty.Register<CardControl, Canvas>(nameof(MainCanvas));
    public Card MyCard
    {
        get => GetValue(MyCardProperty);
        set { SetValue(MyCardProperty, value); OnCardChanged(value); }
    }

    public Canvas MainCanvas
    {
        get => GetValue(MainCanvasProperty);
        set => SetValue(MainCanvasProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _image = e.NameScope.Find<Image>("PART_img") ?? throw new ArgumentNullException("Не найдено изоборажение");
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Initialize();
    }

    protected void OnCardChanged(Card card)
    {
        if (card is not null)
        {
            ZIndex = card.ZIndex;
            card.PropertyChanged += MyCard_PropertyChanged;
        }
    }

    protected void Initialize()
    {
        _image.PointerPressed += PointerPressed;
        _image.PointerReleased += PointerReleased;
        _image.PointerEntered += PointerEntered;
        _image.PointerExited += PointerExited;
        _image.PointerMoved += PointerMoved;
        Train = new();

        if (MainCanvas.DataContext is MainViewModel mainViewModel)
        {
            if (MyCard is not null)
            {
                MyDeck = FindCollectionWithThatCard(mainViewModel, MyCard) ?? throw new Exception("Карта не найдена");
            }
            else
            {
                MyDeck = mainViewModel.Deck;
            }
        }
        else
        {
            throw new Exception("Не тот DataContext");
        }

    }

    private void MyCard_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ZIndex")
        {
            ZIndex = MyCard.ZIndex;
        }
        if (e.PropertyName == "IsOpen")
        {
            var Converter = new CardToImageConverter();
            _image.Source = (CroppedBitmap)Converter.Convert(MyCard, typeof(Bitmap), null, CultureInfo.CurrentCulture);
        }
    }


    protected void PointerEntered(object sendes, PointerEventArgs e)
    {
        IsInArea = true;
    }

    protected void PointerExited(object sendes, PointerEventArgs e)
    {
        IsInArea = false;
    }

    protected void PointerPressed(object sender, PointerPressedEventArgs e)
    {
        PreviuosPosition = e.GetPosition(MainCanvas);
        OriginalPosition = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
        originalZindex = MyCard.ZIndex;
        if (sender is Image img && MyCard.IsOpen && IsInArea && PreviuosPosition.X - OriginalPosition.X < 60 && PreviuosPosition.Y - OriginalPosition.Y < 60)
        {
            IsDragging = true;
            if (ZIndex != 0)
            {
                Train.AddRange(FindAttachedCards());
            }
            MyCard.ZIndex = 1;
            int Zindex = 1;
            Train.ForEach(card => { ++Zindex; card.ZIndex = Zindex; });
        }
    }

    protected void PointerMoved(object sender, PointerEventArgs e)
    {
        if (!IsDragging) return;

        if (sender is Image img)
        {
            var pos = e.GetPosition(MainCanvas);
            double deltaX = pos.X - PreviuosPosition.X;
            double deltaY = pos.Y - PreviuosPosition.Y;
            double left = Canvas.GetLeft(this) + deltaX;
            double top = Canvas.GetTop(this) + deltaY;
            Canvas.SetLeft(this, left);
            Canvas.SetTop(this, top);
            foreach (var item in Train)
            {
                deltaX = pos.X - PreviuosPosition.X;
                deltaY = pos.Y - PreviuosPosition.Y;
                left = Canvas.GetLeft(item) + deltaX;
                top = Canvas.GetTop(item) + deltaY;
                Canvas.SetLeft(item, left);
                Canvas.SetTop(item, top);
            }
            PreviuosPosition = pos;
        }
    }

    protected void PointerReleased(object sender, PointerReleasedEventArgs e)
    {
        IsDragging = false;
        if (MainCanvas.DataContext is MainViewModel vm)
        {
            var myLeft = Canvas.GetLeft(this);
            var myTop = Canvas.GetTop(this);
            var closest = GetClosestCardControl(myLeft, myTop);
            var leftClosest = Canvas.GetLeft(closest);
            var topClosest = Canvas.GetTop(closest);
            if (closest.ZIndex == 0 && Math.Abs(myLeft - leftClosest) < 25 && Math.Abs(myTop - topClosest) < 25)
            {
                if (topClosest < 40)
                {

                }
                else
                {
                    if (CardHelp.IsCompatable(MyCard, closest.MyCard))
                    {
                        MyCard.ZIndex = closest.ZIndex;
                        Canvas.SetLeft(this, Canvas.GetLeft(closest));
                        Canvas.SetTop(this, Canvas.GetTop(closest) + 50);
                        int counter = 1;
                        var myNewHome = FindCollectionWithThatCard(vm, closest.MyCard) ?? throw new Exception("Коллекция ближайшей карты не найдена");
                        foreach (var card in myNewHome)
                        {
                            --card.ZIndex;
                        }
                        MyDeck.Remove(MyCard);

                        if (MyDeck != vm.Deck)
                        {
                            foreach (var card in MyDeck)
                            {
                                ++card.ZIndex;
                            }
                            MyDeck[^1].IsOpen = true;
                        }

                        myNewHome.Add(MyCard);
                        MyDeck = myNewHome;
                        var Zindex = MyCard.ZIndex;
                        foreach (var cardctrl in Train)
                        {
                            cardctrl.ZIndex = Zindex + counter;
                            Canvas.SetLeft(cardctrl, Canvas.GetLeft(this));
                            Canvas.SetTop(cardctrl, Canvas.GetTop(this) + 50 * counter);
                            cardctrl.MyDeck.Remove(cardctrl.MyCard);
                            cardctrl.MyDeck = myNewHome;
                            myNewHome.Add(cardctrl.MyCard);
                            ++counter;
                        }
                    }
                    else
                    {
                        RollBack();
                    }
                }
            }
            else
            {
                RollBack();
            }
            Train.Clear();
        }
    }

    protected void RollBack()
    {
        var counter = 1;
        Canvas.SetLeft(this, OriginalPosition.X);
        Canvas.SetTop(this, OriginalPosition.Y);
        MyCard.ZIndex = originalZindex;
        int ZIndex = originalZindex;
        foreach (var cardctrl in Train)
        {
            Canvas.SetLeft(cardctrl, OriginalPosition.X);
            Canvas.SetTop(cardctrl, OriginalPosition.Y + counter * 50);
            cardctrl.ZIndex = originalZindex + counter;
            ++counter;
        }
        Train.Clear();
    }

    protected CardControl GetClosestCardControl(double left, double top)
    {
        double minDiffLeft = 100000;
        double minDiffTop = 100000;
        CardControl closest = new();
        foreach (var card in from c in MainCanvas.Children
                             let card = c as CardControl
                             where card is not null && card.MyCard != null && card != this
                             select card)
        {
            var leftDiff = left - Canvas.GetLeft(card);
            var topDiff = top - Canvas.GetTop(card);
            if (Math.Abs(leftDiff) < minDiffLeft && card.MyCard.IsOpen)
            {
                closest = card;
                minDiffLeft = Math.Abs(leftDiff);
                minDiffTop = Math.Abs(topDiff);
            }
            else if(Math.Abs(leftDiff) == minDiffLeft && card.MyCard.IsOpen && Math.Abs(topDiff) < minDiffTop)
            {
                closest = card;
                minDiffLeft = Math.Abs(leftDiff);
                minDiffTop = Math.Abs(topDiff);
            }
        }
        return closest;
    }

    protected ObservableCollection<Card>? FindCollectionWithThatCard(MainViewModel vm, Card card)
    {
        if (vm.TableDeck1.Any(x => x == card))
        {
            return vm.TableDeck1;
        }
        else if (vm.TableDeck2.Any(x => x == card))
        {
            return vm.TableDeck2;
        }
        else if (vm.TableDeck3.Any(x => x == card))
        {
            return vm.TableDeck3;
        }
        else if (vm.TableDeck4.Any(x => x == card))
        {
            return vm.TableDeck4;
        }
        else if (vm.TableDeck5.Any(x => x == card))
        {
            return vm.TableDeck5;
        }
        else if (vm.TableDeck6.Any(x => x == card))
        {
            return vm.TableDeck6;
        }
        else if (vm.TableDeck7.Any(x => x == card))
        {
            return vm.TableDeck7;
        }
        else if (vm.Deck.Any(x => x == card))
        {
            return vm.Deck;
        }
        else if (vm.AceHeart.Any(x => x == card))
        {
            return vm.AceHeart;
        }
        else if (vm.AceDiamond.Any(x => x == card))
        {
            return vm.AceDiamond;
        }
        else if (vm.AceClub.Any(x => x == card))
        {
            return vm.AceClub;
        }
        else if (vm.AceSpade.Any(x => x == card))
        {
            return vm.AceSpade;
        }
        return null;
    }

    protected IEnumerable<CardControl> FindAttachedCards()
    {
        List<CardControl> controls = [];
        int index = MyDeck.IndexOf(MyCard);
        foreach (var card in MyDeck.Skip(index + 1))
        {
            var control = from children in MainCanvas.Children
                          let cardctrl = children as CardControl
                          where cardctrl is not null && cardctrl.MyCard == card
                          select cardctrl;

            controls.AddRange(control);
        }
        return controls;
    }
}