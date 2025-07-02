using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Application.Filters
{
    public class LoginFilter
    {
        public LoginFilter()
        {
            
        }
        [Required(ErrorMessage = "Kullanıcı Adı Alanı Zorunludur")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre Alanı Zorunludur")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
