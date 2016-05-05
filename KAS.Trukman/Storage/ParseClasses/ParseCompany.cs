using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseCompany
    [ParseClassName("Company")]
    public class ParseCompany : ParseObject
    {
        [ParseFieldName("name")]
        public string Name
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("displayName")]
        public string DisplayName
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("owner")]
        public ParseUser Owner
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("FleetSize")]
        public int FleetSize
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("drivers")]
        public ParseRelation<ParseUser> Drivers
        {
            get { return this.GetRelationProperty<ParseUser>(); }
        }

        [ParseFieldName("dispatchers")]
        public ParseRelation<ParseUser> Dispatchers
        {
            get { return this.GetRelationProperty<ParseUser>(); }
        }

        [ParseFieldName("requesting")]
        public ParseRelation<ParseUser> Requestings
        {
            get { return this.GetRelationProperty<ParseUser>(); }
        }
    }
    #endregion
}
