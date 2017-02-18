using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Stock
{
    public class UsuarioValidation
    {

        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public UsuarioValidation(string nome, string email, string senha, string conSenha)
        {
            Nome = nome;
            Email = email;

           

            Senha = senha;

        }
    }
}