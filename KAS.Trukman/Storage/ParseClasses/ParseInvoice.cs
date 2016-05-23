using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseInvoice
    [ParseClassName("Invoice")]
    public class ParseInvoice : ParseObject
    {
        [ParseFieldName("file")]
        public ParseFile File
        {
            get { return this.GetProperty<ParseFile>(); }
            set { this.SetProperty<ParseFile>(value); }
        }
    }
    #endregion
}
