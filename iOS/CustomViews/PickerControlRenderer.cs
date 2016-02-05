using System;
using UIKit;
using System.Collections.Generic;

namespace Trukman.iOS
{
	public class PickerControlRenderer : UIViewController
	{
		class PickerDataModel : UIPickerViewModel{
			public event EventHandler<EventArgs> ValueChanged;

			public 

		}
		public PickerControlRenderer ()
		{
		}

		public override void viewDidLoad(){
			base.ViewDidLoad ();
			Title = "Picker View"

			
		}
	}
}

