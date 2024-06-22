using BareSurf.Browser;
using BareSurf.Commands;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
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

namespace BareSurf
{
    /// <summary>
    /// Interaction logic for BrowseItem.xaml
    /// </summary>
    public partial class BrowseItem : UserControl
    {
        BrowserVM Model;
        public BrowserCommands bCmd { get; set; }
        public ChromiumWebBrowser BrowserComponent { get; }
        public BrowseItem()
        {
            Model = new BrowserVM();
            Model.WebAddress = StaticText.HomePage;
            this.DataContext = Model;

            InitializeComponent();
            bCmd = new BrowserCommands(Model, Browser, executionText);
            BrowserComponent = Browser;
        }
        public void Load()
        {
            bCmd.BrowsePageExecute();
        }
        public void Load(string url)
        {
            Model.WebAddress = url;
            txtBoxAddress.Text = url;
            bCmd.BrowsePageExecute();
        }
        void LoadPageExecute(object target, ExecutedRoutedEventArgs e)
        {
            
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter && e.Key != System.Windows.Input.Key.Return && e.Key != System.Windows.Input.Key.BrowserRefresh) return;
            Model.WebAddress = txtBoxAddress.Text;
            bCmd.BrowsePageExecute();
        }
        private void WebAddress_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OnKeyDownHandler(sender, e);
        }
        
    }
}
