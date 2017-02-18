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
using Stock.Model;
using System.IO;

namespace Stock
{
    public class CriarBanco
    {
        public string DbPath { get; set; }
        public SQLiteConnection Db { get; set; }

        public CriarBanco()
        {

                DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "StockBanco.db3");
                Db = new SQLiteConnection(DbPath);
                
   
        }
    }
}