using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region AppButton
    public class AppButton : Button
    {
        public AppButton() : base()
        {
            this.BorderRadius = 25;
            this.BackgroundColor = Color.Black;
            this.TextColor = Color.White;
        }
    }
    #endregion
}
