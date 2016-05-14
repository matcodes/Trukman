using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseComcheck
    [ParseClassName("ComcheckRequest")]
    public class ParseComcheck : ParseObject
    {
        [ParseFieldName("Driver")]
        public ParseUser Driver
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("Dispatch")]
        public ParseUser Dispatch
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("State")]
        public int State
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("RequestDatetime")]
        public DateTime RequestDatetime
        {
            get { return this.GetProperty<DateTime>(DateTime.Now); }
            set { this.SetProperty<DateTime>(value); }
        }

        [ParseFieldName("RequestType")]
        public int RequestType
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("Comcheck")]
        public string Comcheck
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Job")]
        public ParseJob Job
        {
            get { return this.GetProperty<ParseJob>(); }
            set { this.SetProperty<ParseJob>(value); }
        }

        [ParseFieldName("Archive")]
        public bool Archive
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty<bool>(value); }
        }
    }
    #endregion
}
