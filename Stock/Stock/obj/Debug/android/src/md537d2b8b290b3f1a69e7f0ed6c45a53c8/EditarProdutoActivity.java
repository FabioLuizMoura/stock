package md537d2b8b290b3f1a69e7f0ed6c45a53c8;


public class EditarProdutoActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Stock.EditarProdutoActivity, Stock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EditarProdutoActivity.class, __md_methods);
	}


	public EditarProdutoActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == EditarProdutoActivity.class)
			mono.android.TypeManager.Activate ("Stock.EditarProdutoActivity, Stock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}