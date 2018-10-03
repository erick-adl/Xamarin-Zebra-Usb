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
    public class LogMessageEvent : Java.Lang.Object
    {

        private String data;

    public LogMessageEvent(String data)
        {
            this.data = data;
        }

        public String getData()
        {
            return data;
        }
    }
}