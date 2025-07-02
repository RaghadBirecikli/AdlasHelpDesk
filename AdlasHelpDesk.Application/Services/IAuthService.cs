using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Application.Services
{
    public interface IAuthService
    {
        Task<ObjectResult<Tokens>> Login(LoginFilter model);

    }
}
