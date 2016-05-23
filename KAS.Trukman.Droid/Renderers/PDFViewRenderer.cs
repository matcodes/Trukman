using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.Droid.Renderers;
using System.ComponentModel;
using System.Net;

[assembly: ExportRenderer(typeof(PDFView), typeof(PDFViewRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region PDFViewRenderer
    public class PDFViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
                this.ShowPDF();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == "Uri")
                this.ShowPDF();
        }

        private void ShowPDF()
        {
            var customWebView = (this.Element as PDFView);
            Control.Settings.AllowUniversalAccessFromFileURLs = true;
            Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", WebUtility.UrlEncode(customWebView.Uri)));
        }
    }
    #endregion
}