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
        
        double itemWidth = carouselView.Width;
        double viewportCenter = itemWidth / 2;
        
        // Only affect visible items
        for (int i = e.FirstVisibleItemIndex; i <= e.LastVisibleItemIndex; i++)
        {
            if (i < 0 || i >= _viewModel.GameCards.Count)
                continue;
        
            var gameCard = _viewModel.GameCards[i];
        
            // Center of this item
            double itemCenter = (i * itemWidth) + (itemWidth / 2);
        
            // Distance from center of screen
            double distance = Math.Abs(viewportCenter - itemCenter);
        
            // Normalize to range [0, 1], 0 = fully visible center, 1 = one item away
            double normalized = Math.Min(1, distance / itemWidth);
        
            // Invert and clamp opacity (you can tweak min/max fade)
            double opacity = 1.0 - (normalized * 0.5); // fades to 0.5
        
            gameCard.Opacity = opacity;
        }
        
        //var carouselView = (CarouselView)sender;
        //
        //if (carouselView == null)
        //{
        //    return;
        //}
        
        // var isGoingBack = e.HorizontalDelta < 0;
        //
        // if (isGoingBack)
        // {
        //     
        // }

        // if we're scrolling right, we need to increase the opacity of the next item only based on the offset
        // if we're scrolling left, we need to decrease the opacity of the current item we are on
        // this needs to be based on the offset
        // width 400

        // get the center width
        // get the width of the index



        // for (var i = min; i <= max; i++)
        // {
        //     var gameCard = _viewModel.GameCards.ElementAtOrDefault(i);
        //     
        //     var widthOffset = (e.HorizontalOffset - carouselView.Width) / (carouselView.Width * i);
        //     widthOffset = Math.Min(widthOffset, 1);
        //     
        //     gameCard.Opacity = widthOffset;
        //     Debug.WriteLine($"{gameCard.Name} {gameCard.Opacity}");
        // }


        // if (firstGameCard != null)
        // {
        //     var opacity = Math.Abs(e.HorizontalOffset / firstWidth);
        //     
        //     firstGameCard.Opacity = Math.Min(opacity, 1);
        // }
        //
        // if (centerGameCard != null)
        // {
        //     var opacity = Math.Abs(e.HorizontalOffset / centerWidth);
        //     
        //     centerGameCard.Opacity = Math.Min(opacity, 1);
        // }
        //
        // if (lastGameCard != null)
        // {
        //     var opacity = Math.Abs(e.HorizontalOffset / lastWidth);
        //     
        //     lastGameCard.Opacity = Math.Min(opacity, 1);
        // }

        // var maxCarouselWidth = carouselView.Width * _viewModel.GameCards.Count;
        //
        // var gameCard = carouselView.CurrentItem as GameCardViewModel;
        //
        // Debug.WriteLine($"CarouselView: {gameCard.Name} {e.FirstVisibleItemIndex} {e.CenterItemIndex} {e.LastVisibleItemIndex}");
    }
}