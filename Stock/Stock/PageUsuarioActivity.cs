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

namespace Stock
{
    [Activity(Label = "PageUsuarioActivity", Theme = "@style/CustomActionBarTheme")]
    public class PageUsuarioActivity : Activity
    {
        private LinearLayout mMenouActionBar;
        private Button mVerificarEstoque;
        private Button mAddProduto;
        private string _idUsuario;
        private CriarBancoProduto mBanco;
        private CriarBanco mBancoUsuario;
        private Produto mProduto;
        private int _contarPreco;
        private int _contarQtd;
        private JanelaEditarUsuarioFregment janelaCadastrar;
        private JanelaCadastrarProdutoFragment cProduto;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.SetCustomView(Resource.Layout.ActionBarMenuUsuario);
            ActionBar.SetDisplayShowCustomEnabled(true);

            SetContentView(Resource.Layout.PageUsuario);
            // Create your application here

            mMenouActionBar = FindViewById<LinearLayout>(Resource.Id.lnMenu);
            mVerificarEstoque = FindViewById<Button>(Resource.Id.btnVerificarEstoque);
            mAddProduto = FindViewById<Button>(Resource.Id.btnAddProduto);

            _idUsuario = Intent.GetStringExtra("id") ?? "Erro ao obter os dados";

            mBanco = new CriarBancoProduto();
            mBancoUsuario = new CriarBanco();
            //var listas = new List<Produto>(mBanco.Db.Table<Produto>().Where(x => x.IdUsuario == _idUsuario));

            //foreach (var lista in listas)
            //{
            //    _contarPreco = _contarPreco + Int32.Parse(lista.Preco);
            //    _contarQtd = _contarQtd + Int32.Parse(lista.Quantidade);
            //}

            var preco = FindViewById<TextView>(Resource.Id.txtValorMercadorias);
            var qtd = FindViewById<TextView>(Resource.Id.txtQtdProdutos);

            preco.Text = _contarPreco.ToString();
            qtd.Text = _contarQtd.ToString();

            mVerificarEstoque.Click += MVerificarEstoque_Click;

            mAddProduto.Click += (object sender, EventArgs e) =>
            {
                //FragmentTransaction fragAction = FragmentManager.BeginTransaction();

                //janelaCadastrar = new JanelaCadastrarFregment();

                //janelaCadastrar.Show(fragAction, "Caixa de fraguimento");

                //janelaCadastrar.mLogarSucesso += JanelaCadastrar_mLogarSucesso;

                FragmentTransaction fragAction = FragmentManager.BeginTransaction();
                cProduto = new JanelaCadastrarProdutoFragment();
                cProduto.Show(fragAction, "Caixa de fraguimento");
                cProduto.compartilharDados += CProduto_compartilharDados;

            };

            mMenouActionBar.Click += (object sender, EventArgs e) =>
            {
                AlertDialog.Builder alerta = new AlertDialog.Builder(this);
                alerta.SetTitle("Deseja editar seu login? ");
                alerta.SetPositiveButton("Sim", (senderAlert, args) =>
                {
                    FragmentTransaction fragTran = FragmentManager.BeginTransaction();
                    int id = Int32.Parse(_idUsuario);
                    janelaCadastrar = new JanelaEditarUsuarioFregment(id);

                    janelaCadastrar.Show(fragTran, "Janela editar");
                    janelaCadastrar.mLogarSucesso += JanelaCadastrar_mLogarSucesso;
                });
                alerta.SetNegativeButton("Não", (senderAlert, args) => { });
                alerta.SetMessage("Altere apenas os campos necessarios !");
                RunOnUiThread(() => { alerta.Show(); });
            };
            //var dados = mBanco.Db.Table<Produto>();
            //TableQuery<Produto> contar = dados.Where(x => x.IdUsuario == _idUsuario);

            //foreach (var conta in contar)
            //{
            //    if(conta.Preco != "")
            //    _contarPreco = _contarPreco + Int32.Parse(conta.Preco);
            //    if (conta.Quantidade != "")
            //        _contarQtd += Int32.Parse(conta.Quantidade);
            //}

            mMenouActionBar.LongClick += MMenouActionBar_LongClick;
        }

        private void MMenouActionBar_LongClick(object sender, View.LongClickEventArgs e)
        {
            Toast.MakeText(this, "Editar Usuario...", ToastLength.Short).Show();
        }

        private void JanelaCadastrar_mLogarSucesso(object sender, QuandoLogarEventArgs e)
        {


            try
            {

                mBancoUsuario.Db.CreateTable<Usuario>();
                var dados = mBancoUsuario.Db.Table<Usuario>();
                var mId = Int32.Parse(_idUsuario);
                var logar = dados.Where(x => x.Id == mId).FirstOrDefault();


                var verificar = dados.Where(x => x.Email == e.Email).FirstOrDefault();


                logar.Email = e.Email != "" ? e.Email : logar.Email;



                logar.Senha = e.Senha != "" ? e.Senha : logar.Senha;

                mBancoUsuario.Db.Update(logar);

            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }



        }

        private void CProduto_compartilharDados(object sender, CompartilharDadosProdutos e)
        {
            try
            {
                mProduto = new Produto();
                mBanco.Db.CreateTable<Produto>();
                mBanco.Db.CreateTable<Produto>();
                var dados = mBanco.Db.Table<Produto>();
                var pesquisarProduto = dados.Where(x => x.Titulo == e.Nome && x.IdUsuario == _idUsuario).FirstOrDefault();
                if (pesquisarProduto == null)
                {
                    mProduto.IdUsuario = _idUsuario;
                    mProduto.Preco = e.Preco;
                    mProduto.Quantidade = e.Quantidade;
                    mProduto.Titulo = e.Nome;
                }
                else
                {
                    throw new Exception("Produto existente...");
                }

                mBanco.Db.Insert(mProduto);
                Toast.MakeText(this, mProduto.Titulo + " foi criado com sucesso", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
        }

        private void MVerificarEstoque_Click(object sender, EventArgs e)
        {
            var activity = new Intent(this, typeof(VerificarEstoqueActivity));
            string extra = _idUsuario;
            activity.PutExtra("id", extra);
            StartActivity(activity);
        }


    }
}