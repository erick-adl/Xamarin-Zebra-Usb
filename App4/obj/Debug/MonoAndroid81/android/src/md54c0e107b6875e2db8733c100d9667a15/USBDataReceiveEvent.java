package md54c0e107b6875e2db8733c100d9667a15;


public class USBDataReceiveEvent
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App4.Events.USBDataReceiveEvent, App4", USBDataReceiveEvent.class, __md_methods);
	}


	public USBDataReceiveEvent ()
	{
		super ();
		if (getClass () == USBDataReceiveEvent.class)
			mono.android.TypeManager.Activate ("App4.Events.USBDataReceiveEvent, App4", "", this, new java.lang.Object[] {  });
	}

	public USBDataReceiveEvent (java.lang.String p0, int p1)
	{
		super ();
		if (getClass () == USBDataReceiveEvent.class)
			mono.android.TypeManager.Activate ("App4.Events.USBDataReceiveEvent, App4", "System.String, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
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
