using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using XboxMock.ViewModels.Entities;

namespace XboxMock.ViewModels.Pages;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<GameCardViewModel> _gameCards = new ObservableCollection<GameCardViewModel> { };

    public HomePageViewModel()
    {
        GameCards.Add(new GameCardViewModel
        {
            Name = "Fall Guys",
            Description = "Available now.",
            ImageSource = "Resources/Images/Games/fall_guys.jpg",
        });
        
        GameCards.Add(new GameCardViewModel
        {
            Name = "Expedition 33",
            Description = "Set sail on a new expedition.",
            ImageSource = "Resources/Images/Games/expedition_33.jpg",
        });

        GameCards.Add(new GameCardViewModel
        {
            Name = "TES IV: Oblivion Remastered",
            Description = "Available now.",
            ImageSource = "Resources/Images/Games/tes_oblivion.jpg",
        });
    }

    public double GameCardSize => (Application.Current?.Windows[0]?.Width ?? 390) - 20;
}