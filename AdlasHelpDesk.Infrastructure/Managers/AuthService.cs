using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationSettingsModel _appsettings;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;

        public AuthService(IMemberRepository memberRepository, IMapper mapper, ICurrentUserService currentUserService, IOptions<ApplicationSettingsModel> appSettings)
        {
            _appsettings = appSettings.Value;
            _mapper = mapper;
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ObjectResult<Tokens>> Login(LoginFilter model)
        {
            var user = await _memberRepository.GetAsync(x => x.UserName == model.Username);
            if (user is null || user.Password != Functions.MD5(model.Password))
                return new ObjectResult<Tokens>(Meta.CustomError(ConstantMessages.EmailOrPasswordError));
            if (user.IsActive == false)
                return new ObjectResult<Tokens>(Meta.CustomError(ConstantMessages.InactiveUser));

            var userModel = _mapper.Map<MemberDto>(user);
            var res = new Tokens()
            {
                SignedMember = userModel,
                IsAuthenticated = true
            };
            return new ObjectResult<Tokens>(Meta.CustomSuccess(ConstantMessages.SuccessfulLogin), res);
        }
    }
}
