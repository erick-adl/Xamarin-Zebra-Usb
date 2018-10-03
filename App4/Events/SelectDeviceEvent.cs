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

namespace App4.Events
{
    public class SelectDeviceEvent : Java.Lang.Object
    {
        private int device;

        public SelectDeviceEvent(int device)
        {
            this.device = device;
        }

        public int getDevice()
        {
            return device;
        }
    }
}