using CommunityToolkit.Mvvm.ComponentModel;

namespace XboxMock.ViewModels.Entities;

public partial class GameCardViewModel : ObservableObject
{
    [ObservableProperty]
    private ImageSource _imageSource;
    
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;
}