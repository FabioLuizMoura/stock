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

namespace Stock
{
    [Activity(Label = "EditarProdutoActivity", Theme = "@style/CustomActionBarTheme")]
    public class EditarProdutoActivity : Activity
    {
        private EditText mNome;
        private CriarBancoProduto mBanco;
        private EditText mPreco;
        private EditText mQtd;
        private Button mMais;
        private Button mMenos;
        private Button mSalvar;
        private string mIdProduto;
        private LinearLayout mVoltar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetCustomView(Resource.Layout.ActionBarVerificarEstoque);
            ActionBar.SetDisplayShowCustomEnabled(true);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.EditarProdutos);
            FindViewById<LinearLayout>(Resource.Id.lnProcurarActionBarEstoque).Visibility = ViewStates.Invisible;
            FindViewById<LinearLayout>(Resource.Id.btnEncontrarProdutoEstoque).Visibility = ViewStates.Invisible;

            mNome = FindViewById<EditText>(Resource.Id.txtEditarProdutoNome);
            mPreco = FindViewById<EditText>(Resource.Id.txtEditarProdutoPreco);
            mQtd = FindViewById<EditText>(Resource.Id.txtEditarProdutoQtd);

            mMais = FindViewById<Button>(Resource.Id.btnMaisEdProduto);
            mMenos = FindViewById<Button>(Resource.Id.btnMenosEdProduto);
            mSalvar = FindViewById<Button>(Resource.Id.btnSalvarEditarProduto);
            mVoltar = FindViewById<LinearLayout>(Resource.Id.lnVoltarActionBarEstoque);

            mIdProduto = Intent.GetStringExtra("id") ?? "Erro ao obter os dados";
            mBanco = new CriarBancoProduto();

            var id = Int32.Parse(mIdProduto);
            var edite = mBanco.Db.Find<Produto>(id);

            mNome.Text = edite.Titulo;
            mPreco.Text = edite.Preco;
            mQtd.Text = edite.Quantidade;

            mMais.Click += MMais_Click;
            mMenos.Click += MMenos_Click;
            mSalvar.Click += MSalvar_Click;
            mVoltar.Click += MVoltar_Click;
            mVoltar.LongClick += MVoltar_LongClick;
            // Create your application here
        }

        private void MVoltar_LongClick(object sender, View.LongClickEventArgs e)
        {
            Toast.MakeText(this, "Voltar...", ToastLength.Short).Show();
        }

        private void MVoltar_Click(object sender, EventArgs e)
        {
            var dados = mBanco.Db.Table<Produto>();
            int id = Int32.Parse(mIdProduto);
            var produto = dados.Where(x => x.Id == id).FirstOrDefault();
            var activity = new Intent(this, typeof(VerificarEstoqueActivity));
            string extra = produto.IdUsuario.ToString();
            activity.PutExtra("id", extra);
            StartActivity(activity);

            this.Finish();
        }

        private void MSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Int32.Parse(mIdProduto);
                var edite = mBanco.Db.Find<Produto>(id);
                edite.Preco = mPreco.Text != "" ? mPreco.Text : edite.Preco;
                edite.Titulo = mNome.Text != "" ? mNome.Text : edite.Titulo;
                edite.Quantidade = mQtd.Text != "" ? mQtd.Text : edite.Titulo;
                mBanco.Db.Update(edite);

                var dado = mBanco.Db.Table<Produto>();
                var produto = dado.Where(x => x.Id == id).FirstOrDefault();

                var activity = new Intent(this, typeof(VerificarEstoqueActivity));
                string extra = produto.IdUsuario.ToString();
                activity.PutExtra("id", extra);
                StartActivity(activity);
                this.Finish();
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
            
        }

        private void MMenos_Click(object sender, EventArgs e)
        {
            if (mQtd.Text == "0")
            {
                int i = Int32.Parse(mQtd.Text);
                mQtd.Text = i.ToString();
            }
            else if (mQtd.Text == "")
            {
                mQtd.Text = "0";
            }
            else
            {
                int i = Int32.Parse(mQtd.Text);
                i--;
                mQtd.Text = i.ToString();


            }
        }

        private void MMais_Click(object sender, EventArgs e)
        {
            if (mQtd.Text == "0")
            {
                int i = Int32.Parse(mQtd.Text);
                i++;
                mQtd.Text = i.ToString();
            }
            else if (mQtd.Text == "")
            {
                mQtd.Text = "1";
            }
            else
            {
                int i = Int32.Parse(mQtd.Text);
                i++;
                mQtd.Text = i.ToString();


            }
        }
    }
}