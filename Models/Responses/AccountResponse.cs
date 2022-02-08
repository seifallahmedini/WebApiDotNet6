namespace WebApi.Models.Responses
{
    public class AccountResponse
    {
        public string Id { get; set; }
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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string civility { get; set; }
        public string profession { get; set; }
        public string studylevel { get; set; }
        public string situation { get; set; }
    }
}
