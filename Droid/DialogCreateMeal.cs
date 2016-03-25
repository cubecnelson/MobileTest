using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Database;

namespace MobileTest.Droid
{
    class DialogCreateMeal : DialogFragment
    {
        private EditText mTitle;
        private EditText mLocation;
        private ImageView mAddImage;
        private Button mSend;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.DialogCreateMeal, container, false);

            mTitle = view.FindViewById<EditText>(Resource.Id.title_text);
            mLocation = view.FindViewById<EditText>(Resource.Id.location_text);
            mAddImage = view.FindViewById<ImageView>(Resource.Id.add_image);
            mAddImage.Click += mAddImageOnClick;
            mSend = view.FindViewById<Button>(Resource.Id.send);

            return view;
        }

        public void mAddImageOnClick(Object o, EventArgs e)
        {
            Toast.MakeText(this.Activity, getLastTakenPhoto(), ToastLength.Short).Show();
            mAddImage.SetImageURI(Android.Net.Uri.Parse(getLastTakenPhoto()));
        }

        public void mSendOnClick(Object o, EventArgs e)
        {
            
        }


        public string getLastTakenPhoto()
        {
            string path = null;

            string[] projection = new string[] {
                MediaStore.Images.ImageColumns.Id,
                MediaStore.Images.ImageColumns.Data,
                MediaStore.Images.ImageColumns.BucketDisplayName,
                MediaStore.Images.ImageColumns.DateTaken,
                MediaStore.Images.ImageColumns.MimeType
            };
            using (ICursor cursor = this.Activity.ManagedQuery(MediaStore.Images.Media.ExternalContentUri, projection, null, null, MediaStore.Images.ImageColumns.DateTaken + " DESC"))
            {
                
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
    }
}