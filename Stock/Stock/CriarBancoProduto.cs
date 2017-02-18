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
using System.IO;
using Stock.Model;

namespace Stock
{
    public class CriarBancoProduto
    {

        public string DbPath { get; set; }
        public SQLiteConnection Db { get; set; }

        public CriarBancoProduto()
        {

            DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "StockBancoProduto.db3");
            Db = new SQLiteConnection(DbPath);
            Db.CreateTable<Produto>();

        }
    }
}