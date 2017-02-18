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

namespace Stock
{

    public class QuandoIniciarUsuario : EventArgs
    {
        private string _email;
        private string _senha;

        public string Email { get { return _email; } set { _email = value; } }
        public string Senha { get { return _senha; } set { _senha = value; } }

        public QuandoIniciarUsuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

    }

    public class JanelaLoginFragment : DialogFragment
    {

        private EditText mEmail;
        private EditText mSenha;
        private Button mBtnLogar;
        public event EventHandler<QuandoIniciarUsuario> quandoInciar;
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

            var view = inflater.Inflate(Resource.Layout.JanelaLogin, container, false);

            mEmail = view.FindViewById<EditText>(Resource.Id.txtEmailUsuario);
            mSenha = view.FindViewById<EditText>(Resource.Id.txtSenhaUsuario);

            mBtnLogar = view.FindViewById<Button>(Resource.Id.btnLogarUsuario);

            mBtnLogar.Click += MBtnLogar_Click;

            return view;
        }

        private void MBtnLogar_Click(object sender, EventArgs e)
        {
            quandoInciar.Invoke(this, new QuandoIniciarUsuario(mEmail.Text, mSenha.Text));
            this.Dismiss();
        }
    }
}