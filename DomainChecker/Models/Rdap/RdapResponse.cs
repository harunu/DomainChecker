namespace DomainChecker.Models.Rdap
{
    public class RdapResponse
    {
        public string? ObjectClassName { get; set; }
        public string? Handle { get; set; }
        public string? LdhName { get; set; }
        public List<string>? Status { get; set; }
        public List<Event>? Events { get; set; }
        public SecureDNS? SecureDNS { get; set; }
        public List<Link>? Links { get; set; }
        public List<Nameserver>? Nameservers { get; set; }
        public List<string>? RdapConformance { get; set; }
        public List<Notice>? Notices { get; set; }
        public List<Entity>? Entities { get; set; }
    }
}
