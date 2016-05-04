using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Classes
{
    #region MenuItem
    public class MenuItem : BaseData
    {
        public MenuItem(VisualCommand command) 
            : base()
        {
            this.Command = command;
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if ((propertyName == "IsEnabled") && (this.Command != null))
                this.Command.IsEnabled = this.IsEnabled;

            base.DoPropertyChanged(propertyName);
        }

        public VisualCommand Command { get; private set; }

        public string Icon
        {
            get { return (string)this.GetValue("Icon"); }
            set { this.SetValue("Icon", value); }
        }

        public string Label
        {
            get { return (string)this.GetValue("Label"); }
            set { this.SetValue("Label", value); }
        }

        public bool IsEnabled
        {
            get { return (bool)this.GetValue("IsEnabled", true); }
            set { this.SetValue("IsEnabled", value); }
        }
    }
    #endregion
}
