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

namespace Stock.Model
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(10)]
        public string IdUsuario { get; set; }
        [MaxLength(25)]
        public string Titulo { get; set; }
        [MaxLength(9)]
        public string Preco { get; set; }
        [MaxLength(15)]
        public string Quantidade { get; set; }

    }
}