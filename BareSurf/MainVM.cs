using BareSurf.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BareSurf
{
    public class MainVM
    {
        public int SelectedIndex { get; set; }
        public IList<BrowserVM> Browsers { get; set; }
        public BrowserVM SelectedBrowser => Browsers?.ElementAt(SelectedIndex)?? new BrowserVM() { WebAddress = "https://duckduckgo.com/" };
        public MainVM()
        {
            Browsers=new List<BrowserVM>();
            SelectedIndex = 0;
            Browsers.Add(new BrowserVM() { WebAddress= "https://duckduckgo.com/" });
        }
    }
}
