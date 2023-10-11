namespace BookWebApp.Repositories.Base.Models
{
    public class SaveResult
    {
        public bool IsSuccessful { get; set; }

        public string StackTrace { get; set; }

        public string ErrorMessage { get; set; }
    }
}
