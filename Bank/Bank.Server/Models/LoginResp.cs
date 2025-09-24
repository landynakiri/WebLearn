namespace Bank.Server.Models
{
    public class LoginResp
    {
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
