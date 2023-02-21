namespace PlaylistSpotify.ViewModels
{
    public abstract class ViewModel : TinyViewModel
    {
        protected Task HandleException(Exception ex)
        {
            Console.WriteLine(ex);

            return Task.CompletedTask;
        }
    }
}
