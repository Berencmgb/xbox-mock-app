using CommunityToolkit.Mvvm.ComponentModel;

namespace XboxMock.ViewModels.Entities;

public partial class NotificationViewModel : ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _imageUrl;
    [ObservableProperty] private DateTime _sentDate;
}