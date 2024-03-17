namespace ASP_66Bit_Test.Entities
{
    public class Footballer : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public Team Team { get; set; }
        public Country Country { get; set; }
    }
}
