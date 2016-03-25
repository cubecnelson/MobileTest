using System;

using UIKit;

namespace MobileTest.iOS
{
	public partial class UploadViewController : UIViewController
	{
		public UploadViewController (IntPtr handle) : base (handle)
		{		
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CancelButton.Clicked += closeHandler;
			NewImageButton.TouchUpInside += newIamgeHandler;

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		void closeHandler (object sender, EventArgs args)
		{
			if (sender == CancelButton) {
				DismissViewController (true, null);
			}
				
		}

		void newIamgeHandler (object sender, EventArgs args)
		{
			// Create a new Alert Controller
			UIAlertController actionSheetAlert = UIAlertController.Create("New Image", "Select an image from below", UIAlertControllerStyle.ActionSheet);

			// Add Actions
			actionSheetAlert.AddAction(UIAlertAction.Create("Take Photo",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item One pressed.")));

			actionSheetAlert.AddAction(UIAlertAction.Create("Choose From Library",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item Two pressed.")));

			actionSheetAlert.AddAction(UIAlertAction.Create("Use Last Photo Taken",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item Three pressed.")));

			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel",UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel button pressed.")));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover!=null) {
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			// Display the alert
			this.PresentViewController(actionSheetAlert,true,null);

		}

	}
}


