using KAS.Trukman.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region BorderedLabel
    public class AppLabel : Label
    {
        #region Static members
        public static readonly BindableProperty AppLabelStyleProperty = BindableProperty.Create("AppLabelStyle", typeof(AppLabelStyles), typeof(AppLabel), AppLabelStyles.InfoWhite);
        #endregion

        public AppLabel() : base()
        {
        }

        public AppLabelStyles AppLabelStyle
        {
            get { return (AppLabelStyles)this.GetValue(AppLabelStyleProperty); }
            set { this.SetValue(AppLabelStyleProperty, value); }
        }
    }
    #endregion
}
