package md536b697d1149323c42cf308fe2826eaf3;


public class LogworkCreateActivity
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
		mono.android.Runtime.register ("TaskManagerAndroid.Activities.Logworks.LogworkCreateActivity, TaskManageAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LogworkCreateActivity.class, __md_methods);
	}


	public LogworkCreateActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LogworkCreateActivity.class)
			mono.android.TypeManager.Activate ("TaskManagerAndroid.Activities.Logworks.LogworkCreateActivity, TaskManageAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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