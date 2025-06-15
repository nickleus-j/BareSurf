using CefSharp;
using System;
using System.Diagnostics;

namespace BareSurf
{
    public class DefaultLifeSpanHandler : ILifeSpanHandler
    {
        public event Action<string> PopupRequest;

        public bool OnBeforePopup(IWebBrowser browser, string sourceUrl, string targetUrl, ref int x, ref int y, ref int width,
            ref int height)
        {
            if (PopupRequest != null)
                PopupRequest(targetUrl);

            return true;
        }

        public void OnBeforeClose(IWebBrowser browser)
        {

        }

        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            if (PopupRequest != null)
                PopupRequest(targetUrl);
            Process.Start(new ProcessStartInfo
            {
                FileName = targetUrl,
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = true
            });
            newBrowser = null;
            return true;
        }
        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            //throw new NotImplementedException();
            
        }

        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return !browser.IsValid;
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            //throw new NotImplementedException();
        }
    }
}
