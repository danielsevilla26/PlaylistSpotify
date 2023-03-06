namespace PlaylistSpotify.ViewModels
{
    public partial class LoginViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        private readonly ISecureStorageService secureStorageService;

        public LoginViewModel(ISpotifyService spotifyService, ISecureStorageService secureStorageService)
        {
            this.spotifyService = spotifyService;
            this.secureStorageService = secureStorageService;
        }

        public async override Task Initialize()
        {
            await base.Initialize();
            IsBusy = true;

            try
            {
                if (await spotifyService.IsSignedIn())
                {
                    await Navigation.NavigateTo("//Home");
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        [ObservableProperty]
        private bool showLogin;

        [RelayCommand]
        private void OpenLogin()
        {
            ShowLogin = true;
        }

        public async Task HandleAuthCode(string authCode)
        {
            var result = await spotifyService.Initialize(authCode);

            if (result)
            {
                await Navigation.NavigateTo("//Home");
            }
        }
    }
}
