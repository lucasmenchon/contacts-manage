﻿using DawnPoets.Data;
using DawnPoets.Models;

namespace DawnPoets.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }

        public UserModel Adicionar(UserModel addUser)
        {


            if (_context.Usuarios != null && _context.Usuarios.Any())
            {
                addUser.DataCadastro = DateTime.Now;
                _context.Usuarios.Add(addUser);
            } 

            _context.SaveChanges();

            return addUser;
        }

        public List<UserModel> BuscarTodos()
        {
            List<UserModel> listUser = new List<UserModel>();

            return _context.Usuarios != null && _context.Usuarios.Any() ? _context.Usuarios.ToList() : listUser;
        }

        public UserModel BuscarPorId(int id)
        {
            UserModel trueUser = new UserModel();

            if (_context.Usuarios != null && _context.Usuarios.Any())
            {
                foreach (UserModel searchUser in _context.Usuarios)
                {
                    if (searchUser.Id == id) trueUser = searchUser;
                }
            }

            return trueUser;
        }

        public UserModel Atualizar(UserModel pUser)
        {
            UserModel updateUser = BuscarPorId(pUser.Id);

            if (updateUser == null) throw new Exception("Houve um erro na atualização do usuário.");

            updateUser.Id = pUser.Id;
            updateUser.Nome = pUser.Nome;
            updateUser.Login = pUser.Login;
            updateUser.Senha = pUser.Senha;
            updateUser.Email = pUser.Email;
            updateUser.Perfil = pUser.Perfil;
            updateUser.DataCadastro = pUser.DataCadastro;
            updateUser.DataAtualizacao = DateTime.Now;

            if (_context.Usuarios != null && _context.Usuarios.Any()) _context.Usuarios.Update(updateUser);

            _context.SaveChanges();

            return updateUser;
        }

        public bool Apagar(int id)
        {
            UserModel deleteUser = BuscarPorId(id);

            if (deleteUser == null) throw new Exception("Houve um erro na Exclusão do usuário.");

            if (_context.Usuarios != null && _context.Usuarios.Any()) _context.Usuarios.Remove(deleteUser);

            _context.SaveChanges();

            return true;
        }
    }
}