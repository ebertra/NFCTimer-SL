using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NFCTimer_SL.Resources;
using NFCTimer_SL.ViewModel;

using Windows.Networking.Proximity;

//using Microsoft.Xna.Framework.Audio;//sound effect

//extend type : ByteArray.AsBuffer()
using System.Runtime.InteropServices.WindowsRuntime;
//encoding/decoding string to ByteArray
using System.Text;

namespace NFCTimer_SL
{
    public partial class MainPage : PhoneApplicationPage
    {
        ProximityDevice device { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //TODO: remove this two lines to the XAML
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            var appId = Windows.ApplicationModel.Store.CurrentApp.AppId;
            
            //Subscribe to NFC Events
            //TODO: all this should be in a separate, reusable class
            Loaded += MainPage_Loaded;
        }

        //TO DO: Move to a real URI Mapper or Route Classs
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("ms_nfp_launchargs", out parameter))
            {
                //MessageBox.Show("Congratulation\nYou launch application with a NFC tag.\nParamaters : " + parameter);
                //NavigationContext.QueryString.Remove("ms_nfp_launchargs");
                if (parameter == "action=startstop")
                {
                    MainViewModel mainViewModel = this.DataContext as MainViewModel; 
                    if ((mainViewModel != null) && (mainViewModel.StartStopTimerCommand.CanExecute(null)))
                        mainViewModel.StartStopTimerCommand.Execute(null);
                }
            }
        }

        #region Initialisation
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {


            if (device == null)
            {
                //get default NFC device
                device = ProximityDevice.GetDefault();
                device.DeviceArrived += device_DeviceArrived;
                device.DeviceDeparted += device_DeviceDeparted;

                device.SubscribeForMessage("WindowsUri", WindowsUriHandler);

            }
        }

        void device_DeviceDeparted(ProximityDevice sender)
        {
            //Update the UI
            //beep
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
                    MessageBox.Show("Go to: "+s);
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

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}