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
using Stock.Model;
using Android.Graphics;

namespace Stock
{
    public class QuandoEditarEventArgs : EventArgs
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
        public string Senha { get { return _senha; } set { _senha = value; } }
        public string ConSenha { get { return _conSenha; } set { _conSenha = value; } }

        public QuandoEditarEventArgs(string nome, string email, string senha, string conSenha) : base()
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConSenha = conSenha;

        }

    }
    public class JanelaEditarUsuarioFregment : DialogFragment
    {
        private EditText mEmail;
        private EditText mSenha;
        private EditText mConSenha;
        private Button mSalvar;
        private CriarBanco mBanco;
        private int _id;
        public event EventHandler<QuandoLogarEventArgs> mLogarSucesso;

        public JanelaEditarUsuarioFregment(int id)
        {
            _id = id;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.JanelaCadastrar, container, false);

            mBanco = new CriarBanco();
            var dados = mBanco.Db.Table<Usuario>();
            var nome = dados.Where(x => x.Id == _id).FirstOrDefault();

            mEmail = view.FindViewById<EditText>(Resource.Id.txtEmailNovoUsuario);
            mSenha = view.FindViewById<EditText>(Resource.Id.txtSenhaNovoUsuario);
            mConSenha = view.FindViewById<EditText>(Resource.Id.txtConSenhaNovoUsuario);
            mSalvar = view.FindViewById<Button>(Resource.Id.btnSalvarNovoUsuario);

            mEmail.Text = nome.Email;
            mConSenha.Text = nome.Senha;
            mSenha.Text = nome.Senha;

            mSalvar.Click += MSalvar_Click;

            return view;
        }

        private void MSalvar_Click(object sender, EventArgs e)
        {

            
            var dados = mBanco.Db.Table<Usuario>();
            var nome = dados.Where(x => x.Email == mEmail.Text && x.Id != _id).FirstOrDefault();

            if (nome != null)
            {
                mEmail.Text = "";
                mEmail.Hint = "Nome existente...";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mEmail.SetHintTextColor(cor);
            }
            else if (mSenha.Text != mConSenha.Text)
            {
                mConSenha.Text = "";
                mConSenha.Hint = "As senhas estão diferente...";
                Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                mConSenha.SetHintTextColor(cor);
            }
            else if (mSenha.Text != "" && mSenha.Text.Length < 5)
            {
                if (mSenha.Text.Length < 5)
                {
                    mSenha.Text = ""; mConSenha.Text = "";
                    mSenha.Hint = "Minimo 5 caracteres...";
                    Color cor = new Color(); cor.R = 255; cor.G = 0; cor.B = 0; cor.A = 100;
                    mSenha.SetHintTextColor(cor);
                }
            }
            else
            {
                mLogarSucesso.Invoke(this, new QuandoLogarEventArgs("", mEmail.Text, mSenha.Text, mConSenha.Text));
                this.Dismiss();
            }

            
        }
    }
}