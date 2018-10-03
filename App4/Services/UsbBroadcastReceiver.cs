using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using System;

namespace App4.Services
{
    [BroadcastReceiver(Enabled = true, Exported = true, Permission = "android.permission.USB_PERMISSION")]
    [UsesLibrary("android.hardware.usb.host")]
    [IntentFilter(new string[] { "android.hardware.usb.action.USB_DEVICE_ATTACHED", "android.hardware.usb.action.USB_DEVICE_DETACHED", "com.google.android.HID.action.USB_PERMISSION" })]
    public class UsbBroadcastReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent != null)
            {
                UsbDevice device = intent.GetParcelableExtra(UsbManager.ExtraDevice) as UsbDevice;

                String action = intent.Action;
                if (Consts.ACTION_USB_PERMISSION.Equals(action))
                {
                    AbstractUSBHIDService.Instance?.SetDevice(intent, device);
                }
                if (UsbManager.ActionUsbDeviceAttached.Equals(action))
                {
                    AbstractUSBHIDService.Instance?.OnAttachUSB(device);
                }
                if (UsbManager.ActionUsbDeviceDetached.Equals(action))
                {
                    AbstractUSBHIDService.Instance?.DeAttachUSB();
                }
            }

        }
    }
}