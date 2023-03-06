namespace PlaylistSpotify.Services
{
    public interface ISpotifyService
    {
        Task<bool> Initialize(string authCode, bool isRefres = false);

        Task<bool> IsSignedIn();

        Task<SearchResult> Search(string searchText, string types);

        Task<Artist> GetArtist(string id);

        Task<Albums> GetAlbums(string artistId);

        Task<Root> GetCurrentPlaylistUSer();
    }
}
