namespace PlaylistSpotify.Services
{
    public interface ISpotifyService
    {
        Task<bool> Initialize(string authCode);

        Task<SearchResult> Search(string searchText, string types);
    }
}
