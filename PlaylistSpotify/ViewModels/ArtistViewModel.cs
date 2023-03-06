using System.Collections.ObjectModel;

namespace PlaylistSpotify.ViewModels
{
    public partial class ArtistViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public ArtistViewModel(ISpotifyService spotifyService) {
            this.spotifyService = spotifyService;
        }

        public override async Task OnParameterSet()
        {
            try
            {
                IsBusy = true;

                await base.OnParameterSet();
                var id = NavigationParameter.ToString();

                var artistTask = spotifyService.GetArtist(id);
                var albumTask = spotifyService.GetAlbums(id);

                await Task.WhenAll(artistTask, albumTask);

                var artist = artistTask.Result;
                //var album = albumTask.Result;

                TopImage = artist.Images.Any() ? artist.Images.First().Url : null;
                Name = artist.Name;
                Followers = artist.Followers.Total;

                var albumResult = albumTask.Result.Items.Select(x => new SearchItemViewModel
                {
                    Id = x.Id,
                    Title = x.Name,
                    SubTitle = string.Join(",", x.Artists.Select(x => x.Name)),
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null
                });

                Albums = new(albumResult);
            }
            catch(Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;            
        }

        [ObservableProperty]
        private string topImage, name;

        [ObservableProperty]
        private int followers;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> albums = new();
    }
}
