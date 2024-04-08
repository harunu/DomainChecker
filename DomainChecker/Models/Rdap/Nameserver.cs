namespace DomainChecker.Models.Rdap
{
     public class Nameserver
    {
        public string? ObjectClassName { get; set; }
        public string? LdhName { get; set; }
        public List<string>? Status { get; set; }
    }
}
