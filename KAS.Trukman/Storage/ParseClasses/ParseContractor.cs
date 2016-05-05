using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseContractor
    [ParseClassName("Contractor")]
    public class ParseContractor : ParseObject
    {
        [ParseFieldName("Name")]
        public string Name
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Phone")]
        public string Phone
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Fax")]
        public string Fax
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Address")]
        public string Address
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }
    }
    #endregion
}
