using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using BareSurf.Commands;
using CefSharp;
using CefSharp.Wpf;

namespace BareSurf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddTab(StaticText.HomePage);
        }


        private void btnNewTab_Click(object sender, RoutedEventArgs e)
        {
            AddTab(StaticText.HomePage);
        }

        private void btnCloseTab_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.Items.Count > 1)
            {
                tabControl.Items.Remove(tabControl.SelectedItem);
            }
            btnCloseTab.IsEnabled = tabControl.Items.Count > 1;
        }

        public void AddTab(string url)
        {
            var tab = new TabItem
            {
                Header = StaticText.BrowseLbl
            };

            BrowseItem browser = new BrowseItem();
            tab.Content = browser;
            tabControl.Items.Add(tab);
            tabControl.SelectedItem = tab;
            browser.BrowserComponent.TitleChanged += Browser_TitleChanged;
            browser.Load();
            btnCloseTab.IsEnabled = tabControl.Items.Count > 1;
            browser.BrowserComponent.LifeSpanHandler = new DefaultLifeSpanHandler();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabItem = tabControl.SelectedItem as TabItem;
            var browser = tabItem?.Content as BrowseItem;
            browser?.Focus();
        }
        private void Browser_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tabItem = tabControl.SelectedItem as TabItem;
            if(tabItem != null)
            tabItem.Header = e.NewValue;
        }
    }

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
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = targetUrl,
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
            //throw new NotImplementedException();
            return true;
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            //throw new NotImplementedException();
        }
    }
}
