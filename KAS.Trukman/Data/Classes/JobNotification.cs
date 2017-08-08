using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region JobNotification
    public class JobNotification : MainData
    {
        public string Text
        {
            get { return (string)this.GetValue("Text"); }
            set { this.SetValue("Text", value); }
        }

        public Trip Trip
        {
            get { return (this.GetValue("Trip") as Trip); }
            set { this.SetValue("Trip", value); }
        }

        public bool IsSending
        {
            get { return (bool)this.GetValue("IsSending", false); }
            set { this.SetValue("IsSending", value); }
        }

        public bool IsReading
        {
            get { return (bool)this.GetValue("IsReading", false); }
            set { this.SetValue("IsReading", value); }
        }

        public User Sender
        {
            get { return (this.GetValue("Sender") as User); }
            set { this.SetValue("Sender", value); }
        }

        public User Receiver
        {
            get { return (this.GetValue("Receiver") as User); }
            set { this.SetValue("Receiver", value); }
        }

        public DateTime Time
        {
            get { return (DateTime)this.GetValue("Time", DateTime.MinValue); }
            set { this.SetValue("Time", value); }
        }
    }
    #endregion
}
