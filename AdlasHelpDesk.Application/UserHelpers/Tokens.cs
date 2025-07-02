
namespace AdlasHelpDesk.Application.UserHelpers
{
    public class Tokens
    {
        public string? Token { get; set; }
        public MemberDto? SignedMember { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public bool IsAuthenticated { get; set; } = false;
    }
}
