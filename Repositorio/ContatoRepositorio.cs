using DawnPoets.Data;
using DawnPoets.Models;

namespace DawnPoets.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            if (_context.Contatos != null && _context.Contatos.Any()) _context.Contatos.Add(contato);

            _context.SaveChanges();

            return contato;
        }

        public List<ContatoModel> BuscarTodos()
        {
            List<ContatoModel> contatosList = new List<ContatoModel>();

            return _context.Contatos != null && _context.Contatos.Any() ? _context.Contatos.ToList() : contatosList;
        }

        public ContatoModel BuscarPorId(int id)
        {
            ContatoModel contatoModel = new ContatoModel();

            if (_context.Contatos != null && _context.Contatos.Any())
            {
                foreach (ContatoModel contato in _context.Contatos)
                {
                    if (contato.Id == id) contatoModel = contato;
                }
            }

            return contatoModel;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = BuscarPorId(contato.Id);

            if (contatoDB == null) throw new Exception("Houve um erro na atualização do contato.");

            contatoDB.Id = contatoDB.Id;
            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            if (_context.Contatos != null && _context.Contatos.Any()) _context.Contatos.Update(contatoDB);

            _context.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDb = BuscarPorId(id);

            if (contatoDb == null) throw new Exception("Houve um erro na Exclusão do contato.");

            if (_context.Contatos != null && _context.Contatos.Any()) _context.Contatos.Remove(contatoDb);

            _context.SaveChanges();

            return true;
        }
    }
}
