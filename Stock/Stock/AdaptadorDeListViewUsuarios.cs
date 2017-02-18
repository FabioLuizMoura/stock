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

namespace Stock
{
    class AdaptadorDeListViewUsuarios : BaseAdapter<Usuario>
    {
        public List<Usuario> mItens;
        private Context mContexto;
        public AdaptadorDeListViewUsuarios(Context contexto, List<Usuario> itens)
        {
            mItens = itens;
            mContexto = contexto;
        }
        public override Usuario this[int position]
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
            if (view == null)
            {
                view = LayoutInflater.From(mContexto).Inflate(Resource.Layout.AdapterViewAdm, null, false);
            }

            TextView txtUsuarioAdm = view.FindViewById<TextView>(Resource.Id.txtUsuarioAdm);
            txtUsuarioAdm.Text = mItens[position].Email;

            return view;
        }
    }
}