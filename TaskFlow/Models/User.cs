namespace TaskFlow.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Sprint> Sprints { get; set; }
    }
}
