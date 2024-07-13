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

namespace BareSurf
{
    /// <summary>
    /// Interaction logic for BrowserCompo.xaml
    /// </summary>
    public partial class BrowserCompo : UserControl
    {
        public BrowserCompo()
        {
            InitializeComponent();
            btnCloseTab.IsEnabled = tabControl.Items.Count > 1;
            //AddTab("https://duckduckgo.com");
        }
        private void btnNewTab_Click(object sender, RoutedEventArgs e)
        {
            AddTab("https://duckduckgo.com");
        }

        private void btnCloseTab_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.Items.Count > 1)
            {
                var toRemove = tabControl.SelectedItem;
                int selectedIndexAdjustment = tabControl.SelectedIndex == 0 ? 1 : tabControl.SelectedIndex - 1;
                tabControl.SelectedIndex = selectedIndexAdjustment;
                tabControl.Items.Remove(toRemove);
            }
            else
            {
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
            if (tabItem != null)
            {
                tabItem.Header = e.NewValue;
            }
        }

        private void Default_Click(object sender, RoutedEventArgs e)
        {
            BrowseItem browser = new BrowseItem();
            browser.Load("https://duckduckgo.com");
            StartTab.Content = browser;
        }
        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            BrowseItem browser = new BrowseItem();
            browser.Load("https://duckduckgo.com/?q=1&ia=chat");
            StartTab.Content = browser;

        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter && e.Key != System.Windows.Input.Key.Return && e.Key != System.Windows.Input.Key.BrowserRefresh) return;
            LoadPageExecute();
        }
        void LoadPageExecute(object sender, RoutedEventArgs e)
        {
            LoadPageExecute();
        }
        void LoadPageExecute()
        {
            AddTab(txtBoxAddress.Text);
        }
    }
}
