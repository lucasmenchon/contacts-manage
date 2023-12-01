using ContactsManage.Models;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace ContactsManage.Helper
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public User FindSession()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<User>(sessaoUsuario);
        }

        public void CreateSession(User user)
        {
            string sessaoUsuario = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("LoggedInUser", sessaoUsuario);
        }

        public void RemoveSession()
        {
            _httpContext.HttpContext.Session.Remove("LoggedInUser");
        }
    }
}
