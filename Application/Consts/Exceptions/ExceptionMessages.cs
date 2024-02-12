using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consts.Exceptions
{
    public class ExceptionMessages
    {
        public const string AuthanticateFailed = "Kullanıcı adı veya şifre hatalı...";
        public const string UserCreateFailed = "Kullanıcı oluşturulurken bir problem ile karşılaşıldı.";
        public const string AuthAuthenticationFailed = "Login sırasında bir hata ile karşılaşıldı.";
        public const string NotFoundUser = "Kullanıcı bulunamadı.";
        public const string RoleNameControl = "Aynı isimde role bulunmakta";
        public const string WrongPassword = "Şifre Hatalı";


    }
}
