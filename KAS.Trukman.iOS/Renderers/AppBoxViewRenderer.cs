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
    }
    #endregion
}
