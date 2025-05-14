using XboxMock.ViewModels.Pages;

namespace XboxMock;

public partial class AppShell : Shell
{
    private readonly AppShellPageViewModel _viewModel;
    
    public AppShell(AppShellPageViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }
}