using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Classes
{
    #region CommandItem
    public class CommandItem : MenuItem
    {
        public CommandItem(VisualCommand command)
            : base(command)
        {
        }

        public string Description
        {
            get { return (string)this.GetValue("Description"); }
            set { this.SetValue("Description", value); }
        }
    }
    #endregion
}
