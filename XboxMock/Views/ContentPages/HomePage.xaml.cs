using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XboxMock.ViewModels.Entities;
using XboxMock.ViewModels.Pages;

namespace XboxMock.Views.ContentPages;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;
    
    /// <summary>
    /// The amount to scroll to calculate the opacity
    /// </summary>
    private readonly int _scrollAmount = 100;
    
    private readonly Color _backgroundColor = Color.FromArgb("#161716");

    public Color NavbarColor
    {
        get => (Color)GetValue(NavbarColorProperty);
        set => SetValue(NavbarColorProperty, value);
    }
    
    public static readonly BindableProperty NavbarColorProperty = BindableProperty
        .Create(nameof(NavbarColor),
            typeof(Color),
            typeof(HomePage),
            Colors.Transparent);
    
    public HomePage(HomePageViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await _viewModel.RefreshDataAsync();
    }

    private void ItemsView_OnScrolled(object? sender,
        ItemsViewScrolledEventArgs e)
    {
        var carouselView = (CarouselView)sender;
        
        if (carouselView == null)
        {
            return;
        }

        var shouldContinue = e.FirstVisibleItemIndex != e.LastVisibleItemIndex;

        if (!shouldContinue)
        {
            return;
        }
        
        var itemWidth = carouselView.Width;
        var lastItem = _viewModel.GameCards.ElementAtOrDefault(e.LastVisibleItemIndex);
        
        var widthOffset = itemWidth; // always one card width
        var rolledOffset = ((e.HorizontalOffset % itemWidth) + itemWidth) % itemWidth;

        // percent along this card
        var percent = rolledOffset / widthOffset;

        Debug.WriteLine($"Percent: {percent} {e.LastVisibleItemIndex}");

        lastItem.Opacity = percent;

        for (var i = 0; i < _viewModel.GameCards.Count; i++)
        {
            if (i == e.FirstVisibleItemIndex)
            {
                continue;
            }

            if (i == e.CenterItemIndex)
            {
                continue;
            }

            if (i == e.LastVisibleItemIndex)
            {
                continue;
            }

            if (i < e.LastVisibleItemIndex)
            {
                continue;
            }

            var item = _viewModel.GameCards.ElementAtOrDefault(i);

            if (item == null)
            {
                continue;
            }

            item.Opacity = 0;
        }
    }
}