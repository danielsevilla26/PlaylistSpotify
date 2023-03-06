namespace PlaylistSpotify.Views;

public partial class LibraryView : ContentPage
{
	private readonly LibraryViewModel libraryViewModel;

    public LibraryView(LibraryViewModel libraryViewModel)
	{
		InitializeComponent();
		this.libraryViewModel = libraryViewModel;
		BindingContext = libraryViewModel;
	}
}