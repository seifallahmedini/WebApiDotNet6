namespace WebApi.Models.Requests
{
    public class UpdateUserStatusRequest
    {
        private bool _enabled;

        public bool Enabled { get => _enabled; set => _enabled = (bool)replaceEmptyBoolWithNull(value); }


        private bool? replaceEmptyBoolWithNull(bool? value)
        {
            // replace empty string with null to make field optional
            return value == null ? null : value;
        }
    }
}
