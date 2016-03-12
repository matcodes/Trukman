
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Trukman.Droid
{
    public class PDFWebView : View
    {
        #region Contructors

        public PDFWebView(Context context)
            : base(context)
        {
            Initialize();
        }

        public PDFWebView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize();
        }

        public PDFWebView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize();
        }

        #endregion

        void Initialize()
        {
        }
    }
}

