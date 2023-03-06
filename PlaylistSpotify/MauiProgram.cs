global using CommunityToolkit.Maui;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
global using Microsoft.Extensions.Logging;
global using PlaylistSpotify.Models;
global using PlaylistSpotify.Services;
global using PlaylistSpotify.ViewModels;
global using PlaylistSpotify.Views;
global using System;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Windows.Input;
global using TinyMvvm;

namespace PlaylistSpotify;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .UseTinyMvvm()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<HomeView>();
        builder.Services.AddTransient<ArtistView>();
        builder.Services.AddTransient<LibraryView>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ArtistViewModel>();
        builder.Services.AddTransient<LibraryViewModel>();

        builder.Services.AddSingleton<ISpotifyService, SpotifyService>();
        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();

        RegisterRoutes();

        return builder.Build();
	}

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("Artist", typeof(ArtistView));
    }
}
