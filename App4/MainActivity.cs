using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views.InputMethods;
using Android.Widget;
using App4.Services;
using System;

namespace App4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {


        private EditText edtlogText;

        public static MainActivity Instance { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            initUI();
            Instance = this;
            StartService(new Intent(this, typeof(AbstractUSBHIDService)));


        }

        private void initUI()
        {
            edtlogText = (EditText)FindViewById(Resource.Id.edtlogText);

            edtlogText.Focusable = false;
            edtlogText.ShowSoftInputOnFocus = false;


            var imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(edtlogText.WindowToken, 0);
            imm.HideSoftInputFromInputMethod(edtlogText.WindowToken, HideSoftInputFlags.None);


            Button buttonClear = (Button)FindViewById(Resource.Id.buttonClear);
            buttonClear.Click += ButtonClear_Click;

        }

        private async void ButtonClear_Click(object sender, EventArgs e)
        {
            edtlogText.Text = "";
        }

        public void Instance_OnUsbDataReceiver(object sender, string e)
        {
            RunOnUiThread(() =>
            {
                edtlogText.Text += e;
                Log.Debug("####### DEBUG #######", "######### ESCREVI NO EDIT TEXT!!! ############");
            });
        }
    }
}