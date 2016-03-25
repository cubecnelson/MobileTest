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
	[Register ("ListViewController")]
	partial class ListViewController
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		UIKit.UIImageView previewImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (previewImageView != null) {
				previewImageView.Dispose ();
				previewImageView = null;
			}

			if (Button != null) {
				Button.Dispose ();
				Button = null;
			}
		}
	}
}
