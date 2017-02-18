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
using Stock.Model;

namespace Stock
{
    
    public class QuandoLogarEventArgs : EventArgs
    {
        private string _nome;
        private string _email;
        private string _senha;
        private string _conSenha;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Email { get { return _email; } set { _email = value; } }
        public string Senha { get { return _senha; }set { _senha = value; } }
        public string ConSenha { get { return _conSenha; } set { _conSenha = value; } }

        public QuandoLogarEventArgs(string nome, string email, string senha, string conSenha) : base()
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConSenha = conSenha;

        }

    }

    public class JanelaCadastrarFregment : DialogFragment
    {
        private EditText mEmail;
        private EditText mSenha;
        private EditText mConSenha;
        private Button mSalvar;
        public event EventHandler<QuandoLogarEventArgs> mLogarSucesso;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.JanelaCadastrar, container, false );

            
            mEmail = view.FindViewById<EditText>(Resource.Id.txtEmailNovoUsuario);
            mSenha = view.FindViewById<EditText>(Resource.Id.txtSenhaNovoUsuario);
            mConSenha = view.FindViewById<EditText>(Resource.Id.txtConSenhaNovoUsuario);
            mSalvar = view.FindViewById<Button>(Resource.Id.btnSalvarNovoUsuario);

            mSalvar.Click += MSalvar_Click;

            return view;
        }

        private void MSalvar_Click(object sender, EventArgs e)
        {

            var mBanco = new CriarBanco();
            var dado = mBanco.Db.Table<Usuario>();
            var nome = dado.Where(x => x.Email == mEmail.Text).FirstOrDefault();

            if (mEmail.Text == "")
            {
                mEmail.Hint = "Necessario preenchimento";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mEmail.SetHintTextColor(cor);

            }if(nome != null)
            {
                mEmail.Text = "";
                mEmail.Hint = "Usuario existente!";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mEmail.SetHintTextColor(cor);
            }
            else if (mSenha.Text == "")
            {
                mSenha.Hint = "Necessario preenchimento";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mSenha.SetHintTextColor(cor);
            }else if (mSenha.Text.Length < 5)
            {
                mSenha.Text = ""; mConSenha.Text = "";
                mSenha.Hint = "Minimo 5 caracteres";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mSenha.SetHintTextColor(cor);
            }
            else if (mConSenha.Text == "")
            {
                mConSenha.Hint = "Necessario preenchimento";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mConSenha.SetHintTextColor(cor);

            }
            else if(mConSenha.Text != mSenha.Text)
            {
                mConSenha.Text = "";
                mConSenha.Hint = "As senhas estão diferentes...";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mConSenha.SetHintTextColor(cor);
                

            }
            else
            {
                mLogarSucesso.Invoke(this, new QuandoLogarEventArgs("", mEmail.Text, mSenha.Text, mConSenha.Text));
                this.Dismiss();
            }
            
        }
    }
}