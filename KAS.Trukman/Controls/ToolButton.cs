using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region ToolButton
    public class ToolButton : Button
    {
        #region Static members
        public static BindableProperty ImageSourceNameProperty = BindableProperty.Create("ImageSourceName", typeof(string), typeof(ToolButton), default(string), BindingMode.OneWay);
        #endregion

        public ToolButton()
        {
        }

        public string ImageSourceName
        {
            get { return (string)this.GetValue(ImageSourceNameProperty); }
            set { this.SetValue(ImageSourceNameProperty, value); }
        }
    }
    #endregion
}
