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
using BareSurf.Browser;
using BareSurf.Commands;

namespace BareSurf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected MainVM Model { get; set; }
        public BrowserCommands bCmd { get; set; }
        public MainWindow()
        {
            Model = new MainVM();
            Model.SelectedBrowser.WebAddress = "https://www.duckduckgo.com/";
            this.DataContext = Model;
            
            InitializeComponent();
            bCmd = new BrowserCommands(Model, Browser, executionText);
        }

        void LoadPageExecute(object target, ExecutedRoutedEventArgs e)
        {
            bCmd.BrowsePageExecute();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter && e.Key != System.Windows.Input.Key.Return && e.Key != System.Windows.Input.Key.BrowserRefresh) return;
            Model.SelectedBrowser.WebAddress = txtBoxAddress.Text;
            bCmd.BrowsePageExecute();
        }
        private void WebAddress_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OnKeyDownHandler(sender, e);
        }

        private void ToggleStatusBar(object sender, RoutedEventArgs e)
        {
            Model.ShowBottomStatusBar = !Model.ShowBottomStatusBar;
            BottomStatusBar.Visibility = Model.ShowBottomStatusBar ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
