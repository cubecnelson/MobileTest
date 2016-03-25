// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MobileTest.iOS
{
	[Register ("UploadViewController")]
	partial class UploadViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem CancelButton { get; set; }

		[Outlet]
		UIKit.UIButton NewImageButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (NewImageButton != null) {
				NewImageButton.Dispose ();
				NewImageButton = null;
			}
		}
	}
}
