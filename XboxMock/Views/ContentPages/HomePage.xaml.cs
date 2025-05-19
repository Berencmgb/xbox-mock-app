using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        CalculateNavbarColor();
    }
    
    private void ScrollView_OnScrolled(object? sender,
        ScrolledEventArgs e)
    {
        CalculateNavbarColor();
    }

    private void CalculateNavbarColor()
    {
        // if (ScrollView == null)
        // {
        //     return;
        // }
        //
        // var opacity = (float)(ScrollView.ScrollY / _scrollAmount);
        // NavbarColor = new Color(_backgroundColor.Red, _backgroundColor.Green, _backgroundColor.Blue, opacity);
    }
}