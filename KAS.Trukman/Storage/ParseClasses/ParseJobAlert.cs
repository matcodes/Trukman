using System;
using Parse;

namespace KAS.Trukman.Storage.ParseClasses
{
	#region ParseJobAlert
	[ParseClassName("JobAlert")]
	public class ParseJobAlert : ParseObject
	{
		[ParseFieldName("AlertType")]
		public int AlertType
		{
			get { return this.GetProperty<int> ((int)0); }
			set { this.SetProperty<int> (0); }
		}

		[ParseFieldName("AlertText")]
		public string AlertText
		{
			get { return this.GetProperty<string> (); }
			set { this.SetProperty<string> (value); }
		}

        [ParseFieldName("Company")]
        public ParseCompany Company
        {
            get { return this.GetProperty<ParseCompany>(); }
            set { this.SetProperty<ParseCompany>(value); }
        }

        [ParseFieldName("Job")]
        public ParseJob Job
        {
            get { return this.GetProperty<ParseJob>(); }
            set { this.SetProperty<ParseJob>(value); }
        }

        [ParseFieldName("IsViewed")]
        public bool IsViewed
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }
	}
	#endregion
}

