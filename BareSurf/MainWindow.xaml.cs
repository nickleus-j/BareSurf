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
            Model.SelectedBrowser.WebAddress = "https://github.com/";
            this.DataContext = Model;
            InitializeComponent();

        }
        void LoadPageExecute(object target, ExecutedRoutedEventArgs e)
        {
            Browser.Load(Model.SelectedBrowser.WebAddress);
        }
    }
}
