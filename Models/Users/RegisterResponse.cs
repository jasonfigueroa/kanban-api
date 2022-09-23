namespace WebApi.Models.Users
{
    public class RegisterResponse
    {
        public string Message { get; set; }

        public RegisterResponse(string message)
        {
            Message = message;
        }
    }
}
