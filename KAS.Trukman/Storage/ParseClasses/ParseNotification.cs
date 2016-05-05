using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseNotification
    [ParseClassName("Notification")]
    public class ParseNotification : ParseObject
    {
        [ParseFieldName("text")]
        public string Text
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Job")]
        public ParseJob Trip
        {
            get { return this.GetProperty<ParseJob>(); }
            set { this.SetProperty<ParseJob>(value); }
        }

        [ParseFieldName("isSending")]
        public bool IsSending
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }

        [ParseFieldName("isReading")]
        public bool IsReading
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }

        [ParseFieldName("Sender")]
        public ParseUser Sender
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("Receiver")]
        public ParseUser Receiver
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }
    }
    #endregion
}
