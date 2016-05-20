using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParsePhoto
    [ParseClassName("Photo")]
    public class ParsePhoto : ParseObject
    {
        [ParseFieldName("type")]
        public string Kind
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("job")]
        public ParseJob Job
        {
            get { return this.GetProperty<ParseJob>(); }
            set { this.SetProperty<ParseJob>(value); }
        }

		[ParseFieldName("company")]
		public ParseCompany Company
		{
			get { return this.GetProperty<ParseCompany>(); }
			set { this.SetProperty<ParseCompany>(value); }
		}

		[ParseFieldName("isViewed")]
		public bool IsViewed
		{
			get { return this.GetProperty<bool> (false); }
			set { this.SetProperty<bool> (value); }
		}

        [ParseFieldName("photo")]
        public ParseFile Data
        {
            get { return this.GetProperty<ParseFile>(); }
            set { this.SetProperty<ParseFile>(value); }
        }
    }
    #endregion
}
