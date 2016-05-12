using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace KAS.Esignature
{
//	[Static]
//	//[Verify (ConstantsInterfaceAssociation)]
//	partial interface EsignatureConstants
//	{
//		// extern double version;
//		[Field ("version", "__Internal")]
//		double version { get; }
//
//		// extern const unsigned char [] E_SignatureVersionString;
//		[Field ("E_SignatureVersionString", "__Internal")]
//		IntPtr E_SignatureVersionString { get; }
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
	interface EditPdfViewController : ModalSignDelegate, IUIGestureRecognizerDelegate, IUIScrollViewDelegate, IUIAlertViewDelegate
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
