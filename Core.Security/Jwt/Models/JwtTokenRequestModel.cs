namespace Academy.AuthenticationService.Model;

public class JwtTokenRequestModel
{
    public  string Username { get; set; }
    public  string Password { get; set; }
    public  string Email { get; set; }
    public List<string> Roles { get; set; }
}