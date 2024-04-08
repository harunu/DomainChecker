namespace DomainChecker.Models.Rdap
{
    public class Entity
    {
        public string? ObjectClassName { get; set; }
        public string? Handle { get; set; }
        public List<string>? Roles { get; set; }
        public List<PublicId>? PublicIds { get; set; }
        public List<object>? VcardArray { get; set; }
        public List<Entity>? Entities { get; set; }
        public List<Remark>? Remarks { get; set; }
    }
}
