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

namespace BareSurf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected MainVM Model { get; set; }
        public MainWindow()
        {
            Model = new MainVM();
            Model.SelectedBrowser.WebAddress = "https://www.duckduckgo.com/";
            this.DataContext = Model;
            InitializeComponent();

        }
        void LoadPageExecute(object target, ExecutedRoutedEventArgs e)
        {
            Browser.Load(Model.SelectedBrowser.WebAddress);
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter && e.Key != System.Windows.Input.Key.Return && e.Key != System.Windows.Input.Key.BrowserRefresh) return;

            Browser.Load(txtBoxAddress.Text);
        }
        private void WebAddress_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OnKeyDownHandler(sender, e);
        }
    }
}
