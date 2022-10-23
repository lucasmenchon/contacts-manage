using DawnPoets.Models;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace DawnPoets.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UserModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UserModel>(sessaoUsuario);
        }

        public void CriarSessaoUsuario(UserModel user)
        {
            string sessaoUsuario = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", sessaoUsuario);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}
