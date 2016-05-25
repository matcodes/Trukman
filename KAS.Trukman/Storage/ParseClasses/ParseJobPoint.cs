using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseJobPoint
    [ParseClassName("JobPoint")]
    public class ParseJobPoint : ParseObject
    {
        [ParseFieldName("Text")]
        public string Text
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Value")]
        public int Value
        {
            get { return this.GetProperty<int>(0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("Job")]
        public ParseJob Job
        {
            get { return this.GetProperty<ParseJob>(); }
            set { this.SetProperty<ParseJob>(value); }
        }

        [ParseFieldName("Driver")]
        public ParseUser Driver
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("Company")]
        public ParseCompany Company
        {
            get { return this.GetProperty<ParseCompany>(); }
            set { this.SetProperty<ParseCompany>(value); }
        }
    }
    #endregion
}
