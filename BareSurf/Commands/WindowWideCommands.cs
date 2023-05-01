using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BareSurf.Commands
{
    public class WindowWideCommands: ICommand
    {
        private MainWindow window { get; set; }
        public WindowWideCommands(MainWindow parentWindow) {
            parentWindow = window;
        }

        public event EventHandler? CanExecuteChanged;

        public void AddTab()
        {
            AddTab(StaticText.HomePage);
        }
        public void AddTab(string url)
        {
            var tab = new TabItem
            {
                Header = StaticText.BrowseLbl
            };
            var tabControl = window.tabControl;
            BrowseItem browser = new BrowseItem();
            tab.Content = browser;
            tabControl.Items.Add(tab);
            tabControl.SelectedItem = tab;
            browser.BrowserComponent.TitleChanged += Browser_TitleChanged;
            browser.Load();
            window.btnCloseTab.IsEnabled = tabControl.Items.Count > 1;
            browser.BrowserComponent.LifeSpanHandler = new DefaultLifeSpanHandler();
        }
        private void Browser_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tabItem = window.tabControl.SelectedItem as TabItem;
            if (tabItem != null)
            {
                tabItem.Header = e.NewValue;
                window.Title = tabItem.Header?.ToString();
            }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            AddTab(StaticText.HomePage);
        }
    }
}
