namespace Bank.Server.ViewModels
{
    public class GetUserResp
    {
        public required string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IList<string>? Roles { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
