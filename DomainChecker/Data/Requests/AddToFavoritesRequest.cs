namespace DomainChecker.Data.Requests
{
    public class AddToFavoritesRequest
    {
        public string? DomainName { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
