using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking.Proximity;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;

namespace NFCTimer_SL.View
{
    public class NFCPhoneApplicationPage : PhoneApplicationPage
    {
        ProximityDevice NFCDevice { get; set; }

        public void NFCInitialize()
        {
            Loaded += initPageToListenToNFCDevices;
        }

        #region Initialisation
        void initPageToListenToNFCDevices(object sender, RoutedEventArgs e)
        {
            if (NFCDevice == null)
            {
                NFCDevice = ProximityDevice.GetDefault();
                NFCDevice.DeviceArrived += device_DeviceArrived;
                NFCDevice.DeviceDeparted += device_DeviceDeparted;
                NFCDevice.SubscribeForMessage("WindowsUri", WindowsUriHandler);
            }
        }

        void device_DeviceDeparted(ProximityDevice sender)
        {
            //Update the UI
            //beep is there by default though
        }

        void device_DeviceArrived(ProximityDevice sender)
        {
            //Update the UI
        }

        #endregion

        #region NFC EVENT HANDLER
        //URI message received
        private void WindowsUriHandler(ProximityDevice sender, ProximityMessage message)
        {
            try
            {
                var buffer = message.Data.ToArray();
                var sUri = Encoding.Unicode.GetString(buffer, 0, buffer.Length);

                //remove null charater if present
                if (sUri[sUri.Length - 1] == '\0')
                    sUri = sUri.Remove(sUri.Length - 1);

                var uri = new Uri(sUri);
                string s = "WindowsUri : \n" + uri + "\n\n";
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Go to: " + s);
                    //update the UI
                });
            }
            catch (Exception e)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(e.Message);
                });
            }
        }
        #endregion

    }
}
