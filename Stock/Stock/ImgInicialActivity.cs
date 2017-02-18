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
using System.Threading;
using Stock.Model;

namespace Stock
{
    [Activity(MainLauncher = true, Icon = "@drawable/Icone")]
    public class ImgInicialActivity : Activity
    {
        private TextView mClickMe;
        private LinearLayout mLinearLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);

            ActionBar.Hide();

            SetContentView(Resource.Layout.ImgInicial);
            // Create your application here

            mLinearLayout = FindViewById<LinearLayout>(Resource.Id.lnlInicial);

            mLinearLayout.Click += MLinearLayout_Click;

            mClickMe = FindViewById<TextView>(Resource.Id.txtClickMe);

            CriarBanco usuarioAdm = new CriarBanco();
            usuarioAdm.Db.CreateTable<Usuario>();
            var dado = usuarioAdm.Db.Table<Usuario>();
            var acharUsuario = dado.Where(x => x.Email == "admin" && x.Senha == "admin").FirstOrDefault();
            if(acharUsuario == null)
            {
                Usuario adm = new Usuario() { Email = "admin", Senha = "admin", Nome = "amin"};
                usuarioAdm.Db.Insert(adm);
            }

            Thread thread = new Thread(ThreadStart);
            thread.Start();
        }

        private void MLinearLayout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            this.Finish();
        }

        public void ThreadStart()
        {
            Thread.Sleep(5000);
            RunOnUiThread(() => { mClickMe.Visibility = ViewStates.Visible; });
        }

    }
}