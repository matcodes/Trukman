using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using CoreGraphics;

namespace KAS.SPResizableView
{
	// @interface TRUserResizableView : UIView
	[BaseType (typeof(UIView))]
	interface TRUserResizableView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);


		[Wrap ("WeakDelegate")]
		[NullAllowed]
		TRUserResizableViewDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<TRUserResizableViewDelegate> _Nullable delegate;
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

	// @protocol TRUserResizableViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface TRUserResizableViewDelegate
	{
		// @optional -(void)userResizableViewDidBeginEditing:(TRUserResizableView *)userResizableView;
		[Export ("userResizableViewDidBeginEditing:")]
		void UserResizableViewDidBeginEditing (TRUserResizableView userResizableView);

		// @optional -(void)userResizableViewDidEndEditing:(TRUserResizableView *)userResizableView;
		[Export ("userResizableViewDidEndEditing:")]
		void UserResizableViewDidEndEditing (TRUserResizableView userResizableView);
	}
}
