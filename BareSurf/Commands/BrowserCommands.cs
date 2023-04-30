using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BareSurf.Browser;
using CefSharp;
using CefSharp.Wpf;

namespace BareSurf.Commands
{
    public class BrowserCommands
    {
        public Run statusText { get; set; }
        public ChromiumWebBrowser Browser { get; set; }
        BrowserVM Model { get; set; }
        public BrowserCommands(BrowserVM model, ChromiumWebBrowser _Browser, Run _statusText)
        {
            Model= model;
            Browser= _Browser;
            statusText= _statusText;
        }

        public void BrowseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            statusText.Text = StaticText.Executed;
        }

        public void BrowseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            statusText.Text = StaticText.CanBeDoneLbl;
        }
        private void ErrorPop()
        {
            string html = String.Concat("<h1>",StaticText.FailedToLoad,"</h1>");
            string js = "<script>" + "alert(':(')" + "</script>";

            string fullHtml = String.Concat(html,js);
            Browser.LoadHtml(fullHtml);
        }
        public void BrowsePageExecute()
        {
            if (!IsValidUrl(Model.WebAddress))
            {
                Model.WebAddress = String.Concat(StaticText.DefaultSearchPage, HttpUtility.UrlEncode(Model.WebAddress));
            }
            try
            {
                Browser.Load(Model.WebAddress);
            }
            catch {
                ErrorPop();
            }
        }
        public bool IsValidUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute) && url.IndexOf(".")>0 && url[url.Length-1]!='.';
        }
    }
}
