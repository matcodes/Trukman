using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region AppEntry
    public class AppEntry : Entry
    {
        #region Static members
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create("TapCommand", typeof(ICommand), typeof(TappedLabel), null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create("TapCommandParameter", typeof(object), typeof(TappedLabel), null);
        #endregion

        public AppEntry() : base()
        {
            this.Focused += (sender, args) => {
                if ((this.TapCommand != null) && (this.TapCommand.CanExecute(this.TapCommandParameter)))
                    this.TapCommand.Execute(this.TapCommandParameter);
            };
        }

        public ICommand TapCommand
        {
            get { return (this.GetValue(TapCommandProperty) as ICommand); }
            set { this.SetValue(TapCommandProperty, value); }
        }

        public object TapCommandParameter
        {
            get { return this.GetValue(TapCommandParameterProperty); }
            set { this.SetValue(TapCommandParameterProperty, value); }
        }
    }
    #endregion
}
