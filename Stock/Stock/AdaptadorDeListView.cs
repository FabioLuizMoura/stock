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
    class AdaptadorDeListView : BaseAdapter<Produto>
    {
        public List<Produto> mItens;
        private Context mContexto;
        public AdaptadorDeListView(Context contexto, List<Produto> itens)
        {

            mItens = itens;
            mContexto = contexto;

        }
        public override Produto this[int position]
        {
            get
            {
                return mItens[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItens.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if(view == null)
            {
                view = LayoutInflater.From(mContexto).Inflate(Resource.Layout.AdapterViewProdutos, null, false);
            }

            TextView txtNome = view.FindViewById<TextView>(Resource.Id.txtNomeAdapter);
            txtNome.Text = mItens[position].Titulo;

            TextView txtPreco = view.FindViewById<TextView>(Resource.Id.txtPrecoAdapter);
            txtPreco.Text = mItens[position].Preco;

            TextView txtQuantidade = view.FindViewById<TextView>(Resource.Id.txtQuantidadeAdapter);
            txtQuantidade.Text = mItens[position].Quantidade;

            return view;
        }
    }
}