namespace PlaylistSpotify.Views;

public partial class ArtistView
{
    private readonly ArtistViewModel artistViewModel;

    public ArtistView(ArtistViewModel artistViewModel)
	{
		InitializeComponent();
        this.artistViewModel = artistViewModel;
        BindingContext = artistViewModel;
    }

    
}