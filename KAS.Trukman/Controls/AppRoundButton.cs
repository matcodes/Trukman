using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region AppRoundButton
    public class AppRoundButton : AppButton
    {
        public AppRoundButton() : base()
        {
            this.BorderRadius = 25;
            this.BackgroundColor = Color.Black;
            this.TextColor = Color.White;
        }
    }
    #endregion
}
