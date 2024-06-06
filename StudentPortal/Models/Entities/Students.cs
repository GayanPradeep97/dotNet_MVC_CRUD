namespace StudentPortal.Models.Entities
{
    public class Students
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        public required bool Subscribed { get; set; }
    }
}
