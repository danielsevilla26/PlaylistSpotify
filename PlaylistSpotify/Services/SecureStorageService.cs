namespace PlaylistSpotify.Services
{
    internal class SecureStorageService : ISecureStorageService
    {
        public async Task<bool> Contains(string key)
        {
            var result = await SecureStorage.Default.GetAsync(key);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> Get(string key)
        {
            var result = await SecureStorage.Default.GetAsync(key);

            if (result == null)
            {
                throw new KeyNotFoundException("No key in secure storage");
            }

            return result;
        }

        public async Task Save(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }
    }
}
