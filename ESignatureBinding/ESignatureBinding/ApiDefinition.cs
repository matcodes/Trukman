using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace KAS.Esignature
{
//	[Static]
//	[Verify (ConstantsInterfaceAssociation)]
//	partial interface Constants
//	{
//		// extern double version;
//		[Field ("version", "__Internal")]
//		double version { get; }
//
//		// extern const unsigned char [] E_SignatureVersionString;
//		[Field ("E_SignatureVersionString", "__Internal")]
//		byte[] E_SignatureVersionString { get; }
//	}

	// @protocol ModalSignDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface ModalSignDelegate
	{
		// @required -(void)didDismissModalView;
		[Abstract]
		[Export ("didDismissModalView")]
		void DidDismissModalView ();

		// @required -(void)setDrawingSign:(UIImageView *)signImage;
		[Abstract]
		[Export ("setDrawingSign:")]
		void SetDrawingSign (UIImageView signImage);

		// @required -(void)setStoredSign:(UIImageView *)signImage withImagePath:(NSString *)imagePath;
		[Abstract]
		[Export ("setStoredSign:withImagePath:")]
		void SetStoredSign (UIImageView signImage, string imagePath);
	}

	// @interface SignView : UIViewController
	[BaseType (typeof(UIViewController))]
	interface SignView
	{
		[Wrap ("WeakModalSignDelegate")]
		ModalSignDelegate ModalSignDelegate { get; set; }

		// @property (assign, nonatomic) id<ModalSignDelegate> modalSignDelegate;
		[NullAllowed, Export ("modalSignDelegate", ArgumentSemantic.Assign)]
		NSObject WeakModalSignDelegate { get; set; }

		// -(void)saveDrawingAction:(id)sender __attribute__((ibaction));
		[Export ("saveDrawingAction:")]
		void SaveDrawingAction (NSObject sender);

		// -(void)addDrawingAction:(id)sender __attribute__((ibaction));
		[Export ("addDrawingAction:")]
		void AddDrawingAction (NSObject sender);

		// -(void)clearDrawingAction:(id)sender __attribute__((ibaction));
		[Export ("clearDrawingAction:")]
		void ClearDrawingAction (NSObject sender);
	}

	// @interface SPUserResizableView : UIView
	[BaseType (typeof(UIView))]
	interface SPUserResizableView
	{
		[Wrap ("WeakDelegate")]
		SPUserResizableViewDelegate Delegate { get; set; }

		// @property (unsafe_unretained) id<SPUserResizableViewDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// @property (unsafe_unretained) UIView * contentView;
		[Export ("contentView", ArgumentSemantic.Assign)]
		UIView ContentView { get; set; }

		// @property (nonatomic) CGFloat minWidth;
		[Export ("minWidth")]
		nfloat MinWidth { get; set; }

		// @property (nonatomic) CGFloat minHeight;
		[Export ("minHeight")]
		nfloat MinHeight { get; set; }

		// @property (nonatomic, strong) NSString * itsUrl;
		[Export ("itsUrl", ArgumentSemantic.Strong)]
		string ItsUrl { get; set; }

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

	// @protocol EditPdfDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface EditPdfDelegate
	{
		// @required -(void)getStoredPdfPath:(NSString *)pdfPath;
		[Abstract]
		[Export ("getStoredPdfPath:")]
		void GetStoredPdfPath (string pdfPath);
	}

	// @interface EditPdfViewController : UIViewController <ModalSignDelegate, UIGestureRecognizerDelegate, SPUserResizableViewDelegate, UIScrollViewDelegate, UIAlertViewDelegate>
	[BaseType (typeof(UIViewController))]
	interface EditPdfViewController : ModalSignDelegate, IUIGestureRecognizerDelegate, SPUserResizableViewDelegate, IUIScrollViewDelegate, IUIAlertViewDelegate
	{
		// -(id)initWithULR:(NSURL *)pdfUrl andFileName:(NSString *)fileName;
		[Export ("initWithULR:andFileName:")]
		IntPtr Constructor (NSUrl pdfUrl, string fileName);

		[Wrap ("WeakDelegate")]
		EditPdfDelegate Delegate { get; set; }

		// @property (assign, nonatomic) id<EditPdfDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }
	}
}
