using System;
using Foundation;
using UIKit;

namespace KAS.Trukman.iOS
{
	public class PDFHandler:NSObject
	{
		public PDFHandler ()
		{
		}

		public static String uniqueTimestamp() {
			var date = new NSDate ();
			var timestamp = (int)date.SecondsSinceReferenceDate;

			return timestamp.ToString();
		}

	}
}

