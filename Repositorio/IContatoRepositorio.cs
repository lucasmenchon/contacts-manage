using DawnPoets.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DawnPoets.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato);

        List<ContatoModel> BuscarTodos(int userId); // deixar sem parametro faz realizar a busca de todos

        ContatoModel BuscarPorId(int id);

        ContatoModel Atualizar(ContatoModel contato);

        bool Apagar(int id);
    }
}
