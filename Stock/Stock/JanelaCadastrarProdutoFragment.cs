using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Stock
{

    public class CompartilharDadosProdutos : EventArgs
    {
        public string Nome { get; set; }
        public string Preco { get; set; }
        public string Quantidade { get; set; }
        public CompartilharDadosProdutos(string nome, string preco, string qtd)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = qtd;
        }
    }

    public class JanelaCadastrarProdutoFragment : DialogFragment
    {
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }

        private Button mSave;
        private Button mMais;
        private Button mMenos;
        private EditText mNome;
        private EditText mPreco;
        private EditText mQtd;
        public event EventHandler<CompartilharDadosProdutos> compartilharDados;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.JanelaCadastrarProduto, container, false);

            mSave = view.FindViewById<Button>(Resource.Id.btnSalvarCadastarProduto);
            mMais = view.FindViewById<Button>(Resource.Id.btnMaisQuantidade);
            mMenos = view.FindViewById<Button>(Resource.Id.btnMenosQuantidade);
            mNome = view.FindViewById<EditText>(Resource.Id.txtNomeCadastrarProduto);
            mPreco = view.FindViewById<EditText>(Resource.Id.txtPrecoCadastrarProduto);
            mQtd = view.FindViewById<EditText>(Resource.Id.txtQuantidadeEmEstoque);

            mMais.Click += MMais_Click;
            mMenos.Click += MMenos_Click;

            mSave.Click += MSave_Click;

            return view;
        }

        private void MSave_Click(object sender, EventArgs e)
        {
            if (mNome.Text == "")
            {
                mNome.Hint = "Preencha o campo Nome";
                //string cor = "#FF0000";
                Color red = new Color();
                red.R = 255;
                red.G = 0;
                red.B = 0;
                red.A = 100;
                
                mNome.SetHintTextColor(red);
            }
            else if(mPreco.Text == "")
            {
                mPreco.Hint = "Preencha o campo Preço";
                Color red = new Color();
                red.R = 255;
                red.G = 0;
                red.B = 0;
                red.A = 100;

                mPreco.SetHintTextColor(red);
            }
            else if(mQtd.Text == "")
            {
                mQtd.Hint = "Preencha o campo Quantidade";
                Color red = new Color();
                red.R = 255;
                red.G = 0;
                red.B = 0;
                red.A = 100;

                mQtd.SetHintTextColor(red);
            }
            else
            {
                compartilharDados.Invoke(this, new CompartilharDadosProdutos(mNome.Text, mPreco.Text, mQtd.Text));
                this.Dismiss();
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
            if(mQtd.Text == "0")
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