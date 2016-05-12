using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using CoreGraphics;

namespace KAS.SPResizableView
{
	// @interface SPUserResizableView : UIView
	[BaseType (typeof(UIView))]
	interface SPUserResizableView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		SPUserResizableViewDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<SPUserResizableViewDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, weak) UIView * _Nullable contentView;
		[NullAllowed, Export ("contentView", ArgumentSemantic.Weak)]
		UIView ContentView { get; set; }

		// @property (nonatomic) CGFloat minWidth;
		[Export ("minWidth")]
		nfloat MinWidth { get; set; }

		// @property (nonatomic) CGFloat minHeight;
		[Export ("minHeight")]
		nfloat MinHeight { get; set; }

		// @property (nonatomic) BOOL preventsPositionOutsideSuperview;
		[Export ("preventsPositionOutsideSuperview")]
		bool PreventsPositionOutsideSuperview { get; set; }

		// -(void)hideEditingHandles;
		[Export ("hideEditingHandles")]
		void HideEditingHandles ();

		// -(void)showEditingHandles;
		[Export ("showEditingHandles")]
		void ShowEditingHandles ();
	}

	// @protocol SPUserResizableViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPUserResizableViewDelegate
	{
		// @optional -(void)userResizableViewDidBeginEditing:(SPUserResizableView *)userResizableView;
		[Export ("userResizableViewDidBeginEditing:")]
		void UserResizableViewDidBeginEditing (SPUserResizableView userResizableView);

		// @optional -(void)userResizableViewDidEndEditing:(SPUserResizableView *)userResizableView;
		[Export ("userResizableViewDidEndEditing:")]
		void UserResizableViewDidEndEditing (SPUserResizableView userResizableView);
	}
}