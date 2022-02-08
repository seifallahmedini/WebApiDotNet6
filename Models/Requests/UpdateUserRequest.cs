using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class UpdateUserRequest
    {
        private string _password;

        private string _confirmPassword;

        private string _email;

        private string _phone;

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get => _phone; set => _phone = replaceEmptyWithNull(value); }

        [EmailAddress]
        public string Email { get => _email; set => _email = replaceEmptyWithNull(value); }

        [MinLength(6)]
        public string Password { get => _password; set => _password = replaceEmptyWithNull(value); }

        [Compare("Password")]
        public string ConfirmPassword { get => _confirmPassword; set => _confirmPassword = replaceEmptyWithNull(value); }

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
