using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region TappedLabel
    public class TappedLabel : Label
    {
        #region Static members
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create("TapCommand", typeof(ICommand), typeof(TappedLabel), null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create("TapCommandParameter", typeof(object), typeof(TappedLabel), null);
        #endregion

        public TappedLabel()
            : base()
        {
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            if ((propertyName == "TapCommand") ||
                (propertyName == "TapCommandParameter"))
            {
                this.GestureRecognizers.Clear();
                this.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = this.TapCommand,
                    CommandParameter = this.TapCommandParameter,
                    NumberOfTapsRequired = 1
                });
            }

            base.OnPropertyChanged(propertyName);
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
