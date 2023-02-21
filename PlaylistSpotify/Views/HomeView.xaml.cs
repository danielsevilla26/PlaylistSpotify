namespace PlaylistSpotify.Views;

public partial class HomeView : ContentPage
{
    private readonly HomeViewModel homeViewModel;

    public HomeView(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        this.homeViewModel = homeViewModel;
        BindingContext = homeViewModel;
    }
}