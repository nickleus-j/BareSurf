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
using CefSharp.Wpf;

namespace BareSurf.Commands
{
    public class BrowserCommands
    {
        public Run statusText { get; set; }
        public ChromiumWebBrowser Browser { get; set; }
        MainVM Model { get; set; }
        public BrowserCommands(MainVM model, ChromiumWebBrowser _Browser, Run _statusText)
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

        public void BrowsePageExecute()
        {
            if (!IsValidUrl(Model.SelectedBrowser.WebAddress))
            {
                Model.SelectedBrowser.WebAddress = String.Concat(StaticText.DefaultSearchPage, HttpUtility.UrlEncode(Model.SelectedBrowser.WebAddress));
            }
            Browser.Load(Model.SelectedBrowser.WebAddress);
        }
        public bool IsValidUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute) && url.IndexOf(".")>0 && url[url.Length-1]!='.';
        }
    }
}
