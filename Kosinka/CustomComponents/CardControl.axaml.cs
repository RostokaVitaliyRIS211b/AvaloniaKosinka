using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Input;

using Kosinka.Core.Model;

using System;

namespace Kosinka.CustomComponents;

public partial class CardControl : TemplatedControl
{
    protected Point OriginalPosition;
    protected Point PreviuosPosition;
    public bool IsInArea { get; set; } = false;
    public bool IsDragging { get; set; } = false;
    protected Image _image;

    public static readonly StyledProperty<Card> MyCardProperty = AvaloniaProperty.Register<CardControl, Card>(nameof(MyCard));
    public static readonly StyledProperty<Canvas> MainCanvasProperty = AvaloniaProperty.Register<CardControl, Canvas>(nameof(MainCanvas));
    public Card MyCard
    {
        get => GetValue(MyCardProperty);
        set => SetValue(MyCardProperty, value);
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
        Initialize();
    }

    protected void Initialize()
    {
        _image.PointerPressed += PointerPressed;
        _image.PointerReleased += PointerReleased;
        _image.PointerEntered += PointerEntered;
        _image.PointerExited += PointerExited;
        _image.PointerMoved += PointerMoved;
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
        if (sender is Image img && MyCard.IsOpen && MyCard.Zindex==0)
        {
            IsDragging = true;
            OriginalPosition = e.GetPosition(MainCanvas);
            PreviuosPosition = e.GetPosition(MainCanvas);
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
            PreviuosPosition = pos;
        }
    }

    protected void PointerReleased(object sender, PointerReleasedEventArgs e)
    {
        IsDragging = false;

    }
}