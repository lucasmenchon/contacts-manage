using DawnPoets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DawnPoets.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext (DbContextOptions<BancoContext> options) : base(options)
        {
          
        }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UserModel> Usuarios { get; set; }

    }
}
