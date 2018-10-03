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

namespace App4
{
    public abstract class Consts
    {
        public static String BINARY = "binary";
        public static String INTEGER = "int";
        public static String HEXADECIMAL = "hex";
        public static String TEXT = "text";

        public static String ACTION_USB_PERMISSION = "com.google.android.HID.action.USB_PERMISSION";
        public static String MESSAGE_SELECT_YOUR_USB_HID_DEVICE = "Please select your USB HID device";
        public static String MESSAGE_CONNECT_YOUR_USB_HID_DEVICE = "Please connect your USB HID device";
        public static String RECEIVE_DATA_FORMAT = "receiveDataFormat";
        public static String DELIMITER = "delimiter";
        public static String DELIMITER_NONE = "none";
        public static String DELIMITER_NEW_LINE = "newLine";
        public static String DELIMITER_SPACE = "space";
        public static String NEW_LINE = "\n";
        public static String SPACE = " ";

        public static String ACTION_USB_SHOW_DEVICES_LIST = "ACTION_USB_SHOW_DEVICES_LIST";
        public static String ACTION_USB_DATA_TYPE = "ACTION_USB_DATA_TYPE";
        public static int RESULT_SETTINGS = 7;
        public static String USB_HID_TERMINAL_CLOSE_ACTION = "USB_HID_TERMINAL_EXIT";
        public static String WEB_SERVER_CLOSE_ACTION = "WEB_SERVER_EXIT";
        public static String SOCKET_SERVER_CLOSE_ACTION = "SOCKET_SERVER_EXIT";
        public static int USB_HID_TERMINAL_NOTIFICATION = 45277991;
        public static int WEB_SERVER_NOTIFICATION = 45277992;
        public static int SOCKET_SERVER_NOTIFICATION = 45277993;

        private Consts()
        {
        }
    }
}