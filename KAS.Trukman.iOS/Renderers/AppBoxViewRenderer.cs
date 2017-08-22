using KAS.Trukman.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;
using UIKit;
using KAS.Trukman.iOS.Renderers;
using System.ComponentModel;

[assembly: ExportRendererAttribute(typeof(AppBoxView), typeof(AppBoxViewRenderer))]

namespace KAS.Trukman.iOS.Renderers
{
    #region AppBoxViewRenderer
    public class AppBoxViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            this.Layer.BackgroundColor = new CGColor(240, 240, 240);
            this.Layer.BorderWidth = 1.0f;
            this.Layer.BorderColor = new CGColor(240, 240, 240);
            this.Layer.CornerRadius = 25.0f;
            this.Layer.MasksToBounds = true;
        }
    }
    #endregion
}
