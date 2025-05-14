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
    
    public HomePage(HomePageViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }
}