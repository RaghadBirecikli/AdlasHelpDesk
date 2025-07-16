using Microsoft.Extensions.Options;


namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationSettingsModel _appsettings;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer _localizer;

        public AuthService(IStringLocalizer localizer, IMemberRepository memberRepository, IMapper mapper, ICurrentUserService currentUserService, IOptions<ApplicationSettingsModel> appSettings)
        {
            _appsettings = appSettings.Value;
            _mapper = mapper;
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
            _localizer = localizer;
        }

        public async Task<ObjectResult<Tokens>> Login(LoginFilter model)
        {
            var user = await _memberRepository.GetAsync(x => x.UserName == model.Username);
            if (user is null || user.Password != Functions.MD5(model.Password))
                return new ObjectResult<Tokens>(Meta.CustomError(_localizer["EmailOrPasswordError"]));
            if (user.IsActive == false)
                return new ObjectResult<Tokens>(Meta.CustomError(_localizer["InactiveUser"]));

            var userModel = _mapper.Map<MemberDto>(user);
            var res = new Tokens()
            {
                SignedMember = userModel,
                IsAuthenticated = true
            };
            return new ObjectResult<Tokens>(Meta.CustomSuccess(_localizer["SuccessfulLogin"]), res);
        }
    }
}
