using System.Collections.ObjectModel;

namespace PlaylistSpotify.ViewModels
{
    public partial class LibraryViewModel : ViewModel
    {
        readonly ISpotifyService spotifyService;

        public LibraryViewModel(ISpotifyService spotifyService) 
        {
            this.spotifyService = spotifyService;
            LoadLibraries();
        }

        [ObservableProperty]
        private ObservableCollection<Library> libraries;

        [ObservableProperty]
        private ObservableCollection<Root> playlists;

        public void LoadLibraries()
        {
            Libraries = new ObservableCollection<Library>()
            {
                new Library()
                {
                    Id = 1,
                    Name = "Playlists"
                },
                new Library()
                {
                    Id = 2,
                    Name = "Podcasts & Shows"
                },
                new Library()
                {
                    Id = 3,
                    Name = "Albums"
                },
                new Library()
                {
                    Id = 4,
                    Name = "Artists"
                },
                 new Library()
                {
                    Id = 5,
                    Name = "Downloaded"
                }
            };
        }

        [RelayCommand]
        private async Task GetCurrentPlaylistUser()
        {
            try
            {
                IsBusy = true;

                var result = await spotifyService.GetCurrentPlaylistUSer();                
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }
    }
}
