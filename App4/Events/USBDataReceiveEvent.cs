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
    public class USBDataReceiveEvent : Java.Lang.Object
    {
        private  String data;
    private  int bytesCount;

        public USBDataReceiveEvent(String data, int bytesCount)
        {
            this.data = data;
            this.bytesCount = bytesCount;
        }

        public String getData()
        {
            return data;
        }

        public int getBytesCount()
        {
            return bytesCount;
        }
    }
}