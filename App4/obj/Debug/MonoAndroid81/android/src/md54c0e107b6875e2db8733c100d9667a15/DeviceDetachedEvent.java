package md54c0e107b6875e2db8733c100d9667a15;


public class DeviceDetachedEvent
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App4.Events.DeviceDetachedEvent, App4", DeviceDetachedEvent.class, __md_methods);
	}


	public DeviceDetachedEvent ()
	{
		super ();
		if (getClass () == DeviceDetachedEvent.class)
			mono.android.TypeManager.Activate ("App4.Events.DeviceDetachedEvent, App4", "", this, new java.lang.Object[] {  });
	}

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
