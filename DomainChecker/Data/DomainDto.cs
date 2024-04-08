namespace DomainChecker.Data
{
    public class DomainDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime LastChecked { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsFavorite { get; set; }
    }
}
