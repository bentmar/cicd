namespace cicdApp.PersonApi.Models
{
    public record Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
