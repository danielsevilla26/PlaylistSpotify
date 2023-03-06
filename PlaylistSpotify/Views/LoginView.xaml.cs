using System.Net;

namespace PlaylistSpotify.Views;

public partial class LoginView
{
    public LoginViewModel loginViewModel { get; }

    private readonly string state;

    public LoginView(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this.loginViewModel = loginViewModel;
        state = Guid.NewGuid().ToString();
        BindingContext = loginViewModel;

        Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("UserAgent",
            (handler, webview) =>
            {
                var userAgent = "Chrome";
#if ANDROID
            handler.PlatformView.Settings.UserAgentString = userAgent;
#elif IOS
            handler.PlatformView.CustomUserAgent = userAgent;
#endif
            });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        LoginWeb.Navigating += LoginWeb_Navigating;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        loginViewModel.PropertyChanged -= LoginViewModel_PropertyChanged;
        LoginWeb.Navigating -= LoginWeb_Navigating;
    }

    private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LoginViewModel.ShowLogin))
        {
            var scopes = "playlist-read-private playlist-modify-private";
            var queryString = $"response_type=code&client_id={Constants.SpotifyClientId}&scopes={WebUtility.UrlEncode(scopes)}&redirect_uri={Constants.RedirectUrl}&state={state}";

            LoginWeb.Source = $"https://accounts.spotify.com/authorize?{queryString}";

            Login.TranslationY = this.Height;
            Login.IsVisible = true;
            Login.TranslateTo(Login.X, 100, easing: Easing.Linear);
        }
    }

    private async void LoginWeb_Navigating(object sender, WebNavigatingEventArgs e)
    {
        if (!e.Url.Contains("redirect_uri") && e.Url.Contains("https://listspotify/login"))
        {
            var queryString = e.Url.Split("?").Last();
            var parts = queryString.Split("&");

            var parameters = parts.Select(x => x.Split("=")).ToDictionary(x => x.FirstOrDefault(), x => x.Last());

            var code = parameters["code"];
            var returnState = parameters["state"];

            if (returnState == state && !string.IsNullOrWhiteSpace(code))
            {
                _ = Task.Run(async () => await loginViewModel.HandleAuthCode(code));

                await Login.TranslateTo(Login.X, this.Height, easing: Easing.Linear);
                Login.IsVisible = false;
            }
        }
    }
}