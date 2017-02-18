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
using Stock.Model;
using SQLite;
using Stock;

namespace Stock
{
    [Activity(Label = "AdmActivity", Theme = "@style/CustomActionBarTheme")]
    public class AdmActivity : Activity
    {
        private int _id;
        private ListView mListView;
        private AdaptadorDeListViewUsuarios adapter;
        private CriarBanco mBanco;
        private CriarBancoProduto mBancoProduto;
        private TableQuery<Usuario> mDados;
        private JanelaEditarUsuarioFregment janelaCadastrar;
        private TableQuery<Produto> mDadosProduto;
        private List<Usuario> mItens;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.SetCustomView(Resource.Layout.ActionMenuVerde);
            ActionBar.SetDisplayShowCustomEnabled(true);

            // Create your application here

            SetContentView(Resource.Layout.VerificarEstoque);

            mListView = FindViewById<ListView>(Resource.Id.lvListaDeProdutos);

            mBanco = new CriarBanco();
            mBancoProduto = new CriarBancoProduto();
            mDados = mBanco.Db.Table<Usuario>();
            mDadosProduto = mBancoProduto.Db.Table<Produto>();

            var list = mDados.Where(x => x.Email != "admin").FirstOrDefault();

            mItens = new List<Usuario>(mBanco.Db.Table<Usuario>().Where(x => x.Email != "admin"));

            adapter = new AdaptadorDeListViewUsuarios(this, mItens);

            mListView.Adapter = adapter;

            mListView.ItemClick += MListView_ItemClick;
            mListView.ItemLongClick += MListView_ItemLongClick;

        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Deseja editar esse usuario?");
            alerta.SetPositiveButton("Sim", (senderAlert, args) => {

                
                FragmentTransaction fragTran = FragmentManager.BeginTransaction();
                var id = mItens[e.Position].Id;
                _id = id;
                janelaCadastrar = new JanelaEditarUsuarioFregment(id);

                janelaCadastrar.Show(fragTran, "Janela editar");
                janelaCadastrar.mLogarSucesso += JanelaCadastrar_mLogarSucesso;

            });
            alerta.SetNegativeButton("Não", (senderAlert, args) => {  });
            RunOnUiThread(() => { alerta.Show(); });
        }

        private void JanelaCadastrar_mLogarSucesso(object sender, QuandoLogarEventArgs e)
        {
            try
            {

                mBanco.Db.CreateTable<Usuario>();
                var dados = mBanco.Db.Table<Usuario>();
                var id = _id;
                var logar = dados.Where(x => x.Id == id).FirstOrDefault();

                logar.Email = e.Email != "" ? e.Email : logar.Email;

                logar.Senha = e.Senha != "" ? e.Senha : logar.Senha;

                mBanco.Db.Update(logar);

                this.Recreate();
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }

        }

        private void MListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Deseja apagar esse usuario?");
            var iten = mItens[e.Position];
            var id = iten.Id.ToString();
            alerta.SetMessage("Os produtos relacionados a " + iten.Email + " serão apagados também.");
            alerta.SetPositiveButton("sim", (senderAlert, args) =>
            {

                try
                {
                    var mApagar = mDados.Where(x => x.Id == iten.Id).FirstOrDefault();
                    TableQuery<Produto> mApagarUsuarios = mDadosProduto.Where(x => x.IdUsuario == id);

                    mBanco.Db.Delete(mApagar);

                    if (mApagarUsuarios != null)
                    {
                        foreach (var mApagarUsuario in mApagarUsuarios)
                        {
                            mBancoProduto.Db.Delete(mApagarUsuario);
                        }
                    }


                    this.Recreate();
                    throw new Exception("Apagado com sucesso.");
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
                }



            });
            alerta.SetNegativeButton("Não", (senderAlert, args) =>
            {
                
            });

            RunOnUiThread(() => { alerta.Show(); });
        }

   
    }
}