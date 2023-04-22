using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BareSurf.Browser
{
    public class BrowserVM
    {
        public string Title { get; set; }   
        public string WebAddress { get; set; }
        public Uri uriResult;
        public bool urlValidity
        {
            get
            {
                try
                {
                    return Uri.TryCreate(WebAddress, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                }
                catch { return true; }
                
            }
        }
    }
    public class BrowserTabVM
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}