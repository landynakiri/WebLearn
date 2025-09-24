namespace Bank.Server.Models
{
    public class GetUserResp
    {
        public string Id;
        public string UserName;
        public string Email;
        public IList<string> Roles;
        public DateTime? CreatedAt;
        public DateTime? LastLogin;
    }
}
