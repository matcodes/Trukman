using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region AppButton
    public class AppButton : Button
    {
        #region Static members
        public static BindableProperty AppStyleProperty = BindableProperty.Create("AppStyle", typeof(AppButtonStyle), typeof(AppButton), AppButtonStyle.Normal);
        #endregion

        public AppButton() : base()
        {
            this.BorderRadius = 25;
            this.BorderWidth = 2;
            this.TextColor = Color.White;
            this.BorderColor = Color.Black;
            this.BackgroundColor = Color.Black;
        }

        public AppButtonStyle AppStyle
        {
            get { return (AppButtonStyle)this.GetValue(AppStyleProperty); }
            set { this.SetValue(AppStyleProperty, value); }
        }
    }
    #endregion

    #region AppButtonStyle
    public enum AppButtonStyle
    {
        Normal,
        Left,
        Right
    }
    #endregion
}
