namespace WebApi.Models.Requests
{
    public class UpdateAccountRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Language { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public string Address { get; set; }
        public string Civility { get; set; }
        public string Profession { get; set; }
        public string Studylevel { get; set; }
        public string Situation { get; set; }
    }
}
