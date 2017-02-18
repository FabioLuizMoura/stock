using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using Stock.Model;

namespace Stock
{
    [Activity(MainLauncher = false, Icon = "@drawable/Icone", Theme = "@style/CustomActionBarTheme")]
    public class MainActivity : Activity
    {
        private Button mBtnCadastrar;
        private Button mBtnLogar;
        private Usuario novoUsuario;
        private JanelaCadastrarFregment janelaCadastrar;
        private JanelaLoginFragment janelaLogin;
        private CriarBanco mBanco;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //ActionBar.SetDisplayShowHomeEnabled(false);
            //ActionBar.SetDisplayShowTitleEnabled(false);
            ActionBar.SetCustomView(Resource.Layout.ActionMenuVerde);
            ActionBar.SetDisplayShowCustomEnabled(true);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            mBtnCadastrar = FindViewById<Button>(Resource.Id.btnCadastrar);
            mBtnLogar = FindViewById<Button>(Resource.Id.btnLogar);

            

            CriarBancoDeDados();

            mBtnLogar.Click += (object sender, EventArgs e) => 
            {

               

                FragmentTransaction fragAction = FragmentManager.BeginTransaction();
                janelaLogin = new JanelaLoginFragment();
                janelaLogin.Show(fragAction, "Caixa de Login");
                janelaLogin.quandoInciar += JanelaLogin_quandoInciar;
            };

            mBtnCadastrar.Click += (object sender, EventArgs e) =>
            {
                FragmentTransaction fragAction = FragmentManager.BeginTransaction();
                
                janelaCadastrar = new JanelaCadastrarFregment();
             
                janelaCadastrar.Show(fragAction, "Caixa de fraguimento");

                janelaCadastrar.mLogarSucesso += JanelaCadastrar_mLogarSucesso;
            };

        }

        private void CriarBancoDeDados()
        {
            mBanco = new CriarBanco();
        }

        private void JanelaLogin_quandoInciar(object sender, QuandoIniciarUsuario e)
        {
            try
            {
                mBanco.Db.CreateTable<Usuario>();
                var dados = mBanco.Db.Table<Usuario>();
                var logar = dados.Where(x => x.Email == e.Email && x.Senha == e.Senha).FirstOrDefault();
                if (logar.Email == "admin" && logar.Senha == "admin")
                {
                    StartActivity(typeof(AdmActivity));
                }
                else if (logar != null)
                {
                   

                    var atividade2 = new Intent(this, typeof(PageUsuarioActivity));
                    string extra = logar.Id.ToString();
                    atividade2.PutExtra("id", extra);
                    
                    StartActivity(atividade2);

                }else 
                {
                    Toast.MakeText(this, "Usuario não encontrado", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
            
        }

        private void JanelaCadastrar_mLogarSucesso(object sender, QuandoLogarEventArgs e)
        {

            try
            {
                novoUsuario = new Usuario();

                if (!e.Senha.Equals(e.ConSenha))
                {
                    throw new Exception("As senhas estão diferentes, Por favor tente novamente...");
                }

                mBanco.Db.CreateTable<Usuario>();
                var dados = mBanco.Db.Table<Usuario>();
                var login = dados.Where(x => x.Email == e.Email).FirstOrDefault();

                if(login != null)
                {
                    throw new Exception("Usuario já existente, tente novamente...");
                }else
                {
                    Usuario tbUsuario = new Usuario();
                    tbUsuario.Nome = e.Nome;
                    tbUsuario.Senha = e.Senha;
                    tbUsuario.Email = e.Email;
                    mBanco.Db.Insert(tbUsuario);
                    Toast.MakeText(this, "Contato criado com sucesso", ToastLength.Short).Show();
                }
                //mBanco.Db.Dispose();

            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();

            }

        }

    }
}

