using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace NFCTimer_SL.ViewModel
{
    public class NavigationMessage
    {
        public NavigationContext NavigationContext { get; set; }
        public NavigationEventArgs NavigationEvent { get; set; }
 
        public string VoiceCommand
        {
            get
            {
                if (NavigationContext == null || !NavigationContext.QueryString.ContainsKey("voiceCommandName"))
                    return string.Empty;
                return  NavigationContext.QueryString["voiceCommandName"];
            }
        }
 
        public string NfcLaunchArgs
        {
            get
            {
                if (NavigationContext == null || !NavigationContext.QueryString.ContainsKey("ms_nfp_launchargs"))
                    return string.Empty;
                return NavigationContext.QueryString["ms_nfp_launchargs"];
            }
        }
 
        public bool IsStartedByNfcRequest
        {
            get
            {
                return NavigationEvent != null && NavigationEvent.IsStartedByNfcRequest();
            }
        }
 
        public bool IsStartedByVoiceCommand
        {
            get
            {
                return NavigationContext != null && NavigationContext.QueryString.ContainsKey("voiceCommandName");
            }
       
        }
    }

    public static class NavigationEventArgsExtensions
    {
        public static bool IsStartedByNfcRequest(this NavigationEventArgs e)
        {
            var isStartedByNfcRequest = false;
            if (e.Uri != null)
            {
                isStartedByNfcRequest =
                    e.Uri.ToString()
                     .Contains("ms_nfp_launchargs=");
            }
            return isStartedByNfcRequest;
        }
    }
}
