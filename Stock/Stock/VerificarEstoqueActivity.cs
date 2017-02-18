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

using System.Collections.ObjectModel;
using SQLite;

namespace Stock
{
    [Activity(Label = "VerificarEstoqueActivity", Theme = "@style/CustomActionBarTheme")]
    public class VerificarEstoqueActivity : Activity
    {
        private LinearLayout mBtnPesquisar;
        private EditText mTxtPesquisar;
        private string _pesquisa;
        private LinearLayout mBtnVoltar;
        private string mIdUsuario;
        private ListView mListView;
        private AdaptadorDeListView adapter;
        private List<Produto> mItens;
        private CriarBancoProduto mBanco;
        private ImageView mVoltar;
        private TableQuery<Produto> mDados;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.SetCustomView(Resource.Layout.ActionBarVerificarEstoque);
            ActionBar.SetDisplayShowCustomEnabled(true);

            SetContentView(Resource.Layout.VerificarEstoque);
            // Create your application here

            mIdUsuario = Intent.GetStringExtra("id") ?? "Erro ao obter dados";

            mListView = FindViewById<ListView>(Resource.Id.lvListaDeProdutos);

            mBanco = new CriarBancoProduto();
            mDados = mBanco.Db.Table<Produto>();

            var list = mDados.Where(x => x.IdUsuario == mIdUsuario).FirstOrDefault();

            mItens = new List<Produto>(mBanco.Db.Table<Produto>().Where(x => x.IdUsuario == mIdUsuario));

            adapter = new AdaptadorDeListView(this, mItens);

            mListView.Adapter = adapter;

            mTxtPesquisar = FindViewById<EditText>(Resource.Id.txtEncontrarProdutoEstoque);



            mBtnPesquisar = FindViewById<LinearLayout>(Resource.Id.btnEncontrarProdutoEstoque);

            mBtnPesquisar.Click += MBtnPesquisar_Click;
            mBtnPesquisar.LongClick += MBtnPesquisar_LongClick;

            mListView.ItemClick += MListView_ItemClick;

            mListView.ItemLongClick += MListView_ItemLongClick;

            mBtnVoltar = FindViewById<LinearLayout>(Resource.Id.lnVoltarActionBarEstoque);

            mVoltar = FindViewById<ImageView>(Resource.Id.imgVoltar);

            mVoltar.Click += MVoltar_Click;
            mVoltar.LongClick += MVoltar_LongClick;

            mBtnVoltar.Click += MBtnVoltar_Click;

        }

        private void MBtnPesquisar_LongClick(object sender, View.LongClickEventArgs e)
        {
            Toast.MakeText(this, "Pesquisar produto.", ToastLength.Short).Show();
        }

        private void MVoltar_LongClick(object sender, View.LongClickEventArgs e)
        {
            Toast.MakeText(this, "Voltar.", ToastLength.Short).Show();
        }

        private void MVoltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void MBtnPesquisar_Click(object sender, EventArgs e)
        {
            _pesquisa = mTxtPesquisar.Text;

            if (_pesquisa == "")
            {
                mItens = new List<Produto>(mBanco.Db.Table<Produto>().Where(x => x.IdUsuario == mIdUsuario));
                adapter = new AdaptadorDeListView(this, mItens);
                mListView.Adapter = adapter;
            }
            else
            {
                mItens = new List<Produto>(mBanco.Db.Table<Produto>().Where(x => x.IdUsuario == mIdUsuario));

                var Itens = from x in mBanco.Db.Table<Produto>()
                            where x.Titulo.StartsWith(_pesquisa) && x.IdUsuario == mIdUsuario

                            select x;




                mItens = Itens.ToList<Produto>();

                adapter = new AdaptadorDeListView(this, mItens);
                mListView.Adapter = adapter;
            }

        }

        private void MListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Deseja Apagar? ");
            alerta.SetPositiveButton("Sim", (senderAlert, args) =>
            {

                try
                {
                    var iten = this.mItens[e.Position];
                    //mBanco = new CriarBancoProduto();
                    //mBanco.Db.CreateTable<Produto>();
                    mBanco.Db.Table<Produto>();
                    var id = mDados.Where<Produto>(x => x.Id == iten.Id).FirstOrDefault();
                    //mBanco.Db.CreateTable<Produto>();

                    mBanco.Db.Delete(id);
                    this.Recreate();



                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                }

            });
            alerta.SetNegativeButton("Não", (senderAlert, args) =>
            {
                
            });

            RunOnUiThread(() => { alerta.Show(); });

        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Deseja Editar? ");
            alerta.SetPositiveButton("Sim", (senderAlert, args) =>
            {

                try
                {
                    var iten = this.mItens[e.Position];
                    //mBanco = new CriarBancoProduto();
                    //mBanco.Db.CreateTable<Produto>();

                    var atividade = new Intent(this, typeof(EditarProdutoActivity));
                    string extra = iten.Id.ToString();
                    atividade.PutExtra("id", extra);
                    StartActivity(atividade);
                    this.Finish();



                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                }

            });
            alerta.SetNegativeButton("Não", (senderAlert, args) =>
            {
                
            });

            RunOnUiThread(() => { alerta.Show(); });
        }

        private void MBtnVoltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}