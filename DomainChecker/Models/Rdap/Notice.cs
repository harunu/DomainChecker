namespace DomainChecker.Models.Rdap
{
    public class Notice
    {
        public string? Title { get; set; }
        public List<string>? Description { get; set; }
        public List<Link>? Links { get; set; }
    }
}
