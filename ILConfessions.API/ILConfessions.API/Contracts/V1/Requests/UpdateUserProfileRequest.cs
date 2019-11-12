namespace ILConfessions.API.Contracts.V1.Requests
{
    public class UpdateUserProfileRequest
    {
        public string KnownAs { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}