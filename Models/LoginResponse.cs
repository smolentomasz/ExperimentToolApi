namespace ExperimentToolApi.Models
{
    public class LoginResponse
    {
        public string Username {get;set;}
        public string Token {get;set;}
        public string RefreshToken {get;set;}

        public LoginResponse(User user){
            this.Username = user.Username;
        }
    }
}