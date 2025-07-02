
namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class CurrentUserManager : ICurrentUserService
    {
        private readonly IHttpContextAccessor accessor;

        public CurrentUserManager(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public MemberDto User()
        {
            ClaimsPrincipal claims = accessor?.HttpContext?.User as ClaimsPrincipal;
            return JsonConvert.DeserializeObject<MemberDto>(claims.FindFirst("SignedMember").Value);
        }
    }
}
