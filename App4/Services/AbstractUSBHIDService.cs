using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using App4.Events;

namespace App4.Services
{
    [Service]
    public class AbstractUSBHIDService : Service
    {

        public static AbstractUSBHIDService Instance { get; protected set; }

        #region [ variables service ]
        static string TAG = AbstractUSBHIDService.TAG;

        Handler uiHandler = new Handler();
        Task usbThreadDataReceiver;
        Task verificaPacoteLido;

        public UsbManager mUsbManager;
        public UsbInterface intf;
        public UsbEndpoint endPointRead;
        public UsbEndpoint endPointWrite;
        public UsbDeviceConnection connection;

        IntentFilter filter;
        PendingIntent mPermissionIntent;
        byte[] bufferReaded = new byte[256];

        #endregion [ variables service ]

        public override IBinder OnBind(Intent intent) => null;



        public override void OnCreate()
        {
            Instance = this;

            OnUsbDataReceiver += MainActivity.Instance.Instance_OnUsbDataReceiver;

            mPermissionIntent = PendingIntent.GetBroadcast(this, 0, new Intent(Consts.ACTION_USB_PERMISSION), 0);
            mUsbManager = (UsbManager)GetSystemService(Context.UsbService);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            stopThis();
        }



        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return StartCommandResult.RedeliverIntent;
        }


        public void OnAttachUSB(UsbDevice usbDevice)
        {
            mUsbManager.RequestPermission(usbDevice, mPermissionIntent);
        }


        public void DeAttachUSB()
        {
            stopThis();
        }


        public void stopThis()
        {
            IsReading = false;
        }

        protected volatile bool IsReading = true;
        public void SetDevice(Intent intent, UsbDevice usbDevice)
        {
            int packetSize = 0;
            IsReading = true;

            if (usbDevice != null && intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false))
            {
                connection = mUsbManager.OpenDevice(usbDevice);

                intf = usbDevice.GetInterface(0);
                if (null == connection)
                {
                    Log.Debug("DEBUG", "unable to establish connection)\n");
                }
                else
                {
                    connection.ClaimInterface(intf, true);
                }

                try
                {

                    if (UsbAddressing.DirMask == intf.GetEndpoint(0).Direction)
                    {
                        endPointRead = intf.GetEndpoint(0);
                        packetSize = endPointRead.MaxPacketSize;
                    }
                    else
                    {
                        Log.Debug("####### DEBUG #######", "######### Cagou geral !!! ############");
                    }
                }
                catch (Exception e)
                {
                    Log.Debug("endPointWrite", "Device have no endPointRead" + e);
                }

                var cancellationTokenSource = new CancellationTokenSource();


                new Thread(new ThreadStart(delegate
                {
                    try
                    {
                        if (connection != null && endPointRead != null)
                        {
                            while (IsReading)
                            {
                                byte[] buffer = new byte[packetSize];
                                int status = connection.BulkTransfer(endPointRead, buffer, packetSize, 100);
                                if (status > 0)
                                {
                                    var count = bufferReaded.Count(x => x != 0x00);
                                    Buffer.BlockCopy(buffer, 0, bufferReaded, count, buffer.Length);
                                    Log.Debug("####### DEBUG #######", "COPIEEEI!");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug(TAG, "Error in receive thread" + e.Message);
                    }
                })).Start();

                //usbThreadDataReceiver = Task.Factory.StartNew(a =>
                //{
                //    try
                //    {
                //        if (connection != null && endPointRead != null)
                //        {
                //            while (IsReading)
                //            {
                //                byte[] buffer = new byte[packetSize];
                //                int status = connection.BulkTransfer(endPointRead, buffer, packetSize, 100);
                //                if (status > 0)
                //                {
                //                    var count = bufferReaded.Count(x => x != 0x00);
                //                    Buffer.BlockCopy(buffer, 0, bufferReaded, count, buffer.Length);
                //                }
                //            }
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Log.Debug(TAG, "Error in receive thread" + e.Message);
                //    }

                //}, TaskCreationOptions.LongRunning, cancellationTokenSource.Token );

                new Thread(new ThreadStart(delegate
                {
                    try
                    {
                        while (IsReading)
                        {
                            if (bufferReaded.Count(x => x != 0x00) > 150)
                            {
                                int n = 0;
                                while ((bufferReaded[bufferReaded.Count(x => x != 0x00) - 2] != 0x7c) && (bufferReaded[bufferReaded.Count(x => x != 0x00) - 1] != 0x41))
                                {
                                    if (bufferReaded.Count(x => x != 0x00) >= 255)
                                    {
                                        Array.Clear(bufferReaded, 0, bufferReaded.Length);
                                        break;
                                    }
                                }
                                var count = bufferReaded.Count(x => x != 0x00);

                                StringBuilder stringBuilder = new StringBuilder();
                                int i = 0;

                                for (; i < bufferReaded.Length && bufferReaded[i] != 0; i++)
                                {
                                    stringBuilder.Append(Convert.ToString((char)bufferReaded[i]));
                                }
                                Array.Clear(bufferReaded, 0, bufferReaded.Length);

                                //Task.Factory.StartNew(() =>
                                //{
                                OnUsbDataReceiver?.Invoke(new object(), stringBuilder.ToString());
                                Log.Debug("####### DEBUG #######", stringBuilder.ToString());
                                //}).ConfigureAwait(false);

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug(TAG, "Error verify buffer" + e.Message);
                    }
                })).Start();

                //verificaPacoteLido = Task.Factory.StartNew(a =>
                //{
                //    try
                //    {
                //        while (IsReading)
                //        {
                //            if (bufferReaded.Count(x => x != 0x00) > 150)
                //            {
                //                int n = 0;
                //                while ((bufferReaded[bufferReaded.Count(x => x != 0x00) - 2] != 0x7c) && (bufferReaded[bufferReaded.Count(x => x != 0x00) - 1] != 0x41))
                //                {
                //                    if (bufferReaded.Count(x => x != 0x00) >= 255)
                //                    {
                //                        Array.Clear(bufferReaded, 0, bufferReaded.Length);
                //                        break;
                //                    }
                //                }
                //                var count = bufferReaded.Count(x => x != 0x00);

                //                StringBuilder stringBuilder = new StringBuilder();
                //                int i = 0;

                //                for (; i < bufferReaded.Length && bufferReaded[i] != 0; i++)
                //                {
                //                    stringBuilder.Append(Convert.ToString((char)bufferReaded[i]));
                //                }
                //                Array.Clear(bufferReaded, 0, bufferReaded.Length);

                //                //Task.Factory.StartNew(() =>
                //                //{
                //                //OnUsbDataReceiver?.Invoke(new object(), stringBuilder.ToString());
                //                Log.Debug("####### DEBUG #######", stringBuilder.ToString());
                //                //}).ConfigureAwait(false);

                //            }
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Log.Debug(TAG, "Error verify buffer" + e.Message);
                //    }

                //}, TaskCreationOptions.LongRunning, cancellationTokenSource.Token);
            }
        }

        public void OnUSBDataReceive(byte[] buffer)
        {
            try
            {
                var count = bufferReaded.Count(x => x != 0x00);
                Buffer.BlockCopy(buffer, 0, bufferReaded, count, buffer.Length);
            }
            catch (Exception e)
            {
                Array.Clear(bufferReaded, 0, bufferReaded.Length);
            }
        }

        public event EventHandler<string> OnUsbDataReceiver;


    }
}