package md537d2b8b290b3f1a69e7f0ed6c45a53c8;


public class JanelaEditarUsuarioFregment
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onActivityCreated:(Landroid/os/Bundle;)V:GetOnActivityCreated_Landroid_os_Bundle_Handler\n" +
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Stock.JanelaEditarUsuarioFregment, Stock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", JanelaEditarUsuarioFregment.class, __md_methods);
	}


	public JanelaEditarUsuarioFregment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == JanelaEditarUsuarioFregment.class)
			mono.android.TypeManager.Activate ("Stock.JanelaEditarUsuarioFregment, Stock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public JanelaEditarUsuarioFregment (int p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == JanelaEditarUsuarioFregment.class)
			mono.android.TypeManager.Activate ("Stock.JanelaEditarUsuarioFregment, Stock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public void onActivityCreated (android.os.Bundle p0)
	{
		n_onActivityCreated (p0);
	}

	private native void n_onActivityCreated (android.os.Bundle p0);


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

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
