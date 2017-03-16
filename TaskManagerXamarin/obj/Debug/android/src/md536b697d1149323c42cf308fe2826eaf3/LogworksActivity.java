package md536b697d1149323c42cf308fe2826eaf3;


public class LogworksActivity
	extends android.app.ListActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("TaskManagerAndroid.Activities.Logworks.LogworksActivity, TaskManageAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LogworksActivity.class, __md_methods);
	}


	public LogworksActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LogworksActivity.class)
			mono.android.TypeManager.Activate ("TaskManagerAndroid.Activities.Logworks.LogworksActivity, TaskManageAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

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
