using KAS.Trukman.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;
using UIKit;
using KAS.Trukman.iOS.Renderers;

[assembly: ExportRendererAttribute(typeof(AppBoxView), typeof(AppBoxViewRenderer))]

namespace KAS.Trukman.iOS.Renderers
{
    #region AppBoxViewRenderer
    public class AppBoxViewRenderer : ViewRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (var context = UIGraphics.GetCurrentContext())
            {
                context.SetFillColor(Xamarin.Forms.Color.White.ToCGColor());
                context.SetStrokeColor(Xamarin.Forms.Color.White.ToCGColor());
                context.SetLineWidth((float)1);

                var rc = this.Bounds.Inset(1, 1);

                var path = CGPath.FromRoundedRect(rc, 25, 25);
                context.AddPath(path);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);
        }
    }
    #endregion
}
