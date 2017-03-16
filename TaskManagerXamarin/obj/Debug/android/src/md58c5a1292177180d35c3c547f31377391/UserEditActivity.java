package md58c5a1292177180d35c3c547f31377391;


public class UserEditActivity
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
		mono.android.Runtime.register ("TaskManagerAndroid.Activities.Users.UserEditActivity, TaskManageAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserEditActivity.class, __md_methods);
	}


	public UserEditActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserEditActivity.class)
			mono.android.TypeManager.Activate ("TaskManagerAndroid.Activities.Users.UserEditActivity, TaskManageAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
