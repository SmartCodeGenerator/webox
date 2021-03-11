using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Webox.BLL.Infrastructure
{
    public class AuthOptions
    {
        private const string KEY = "3t8nch4zL8FtIqco7tMx";
        public const string ISSUER = "WeboxServer";
        public const string AUDIENCE = "WeboxClient";
        public const int LIFETIME = 420;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
