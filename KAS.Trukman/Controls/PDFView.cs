using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region PDFView
    public class PDFView : WebView
    {
        #region Static members
        public static readonly BindableProperty UriProperty = BindableProperty.Create("Uri", typeof(string), typeof(WebView), default(string));
        #endregion

        public PDFView() 
            : base()
        {
        }

        public string Uri
        {
            get { return (string)this.GetValue(UriProperty); }
            set { this.SetValue(UriProperty, value); }
        }
    }
    #endregion
}
