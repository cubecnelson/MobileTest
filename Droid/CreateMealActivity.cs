using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Database;
using Java.IO;
using Android.Graphics;
using Android.Content.PM;

namespace MobileTest.Droid
{
    [Activity(Label = "CreateMealActivity")]
    public class CreateMealActivity : Activity
    {

        public static readonly int PickImageId = 1000;
        private EditText mTitle;
        private EditText mLocation;
        private Button mLastPic;
        private Button mCamera;
        private Button mGallery;
        private Button mSend;
        private ImageView mImageView;
        private string path_to_image = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateMeal);

            ActionBar.SetCustomView(Resource.Layout.ActionBarCreateMeal);
            ActionBar.SetDisplayShowCustomEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);


            CreateDirectoryForPictures();

            mTitle = FindViewById<EditText>(Resource.Id.title_text);
            mLocation = FindViewById<EditText>(Resource.Id.location_text);
            mImageView = FindViewById<ImageView>(Resource.Id.add_image);
            mLastPic = FindViewById<Button>(Resource.Id.last_pic);
            mLastPic.Click += mLastPicOnClick;
            mCamera = FindViewById<Button>(Resource.Id.camera);
            mCamera.Click += mCameraOnClick;
            mGallery = FindViewById<Button>(Resource.Id.gallery);
            mGallery.Click += mGalleryOnClick;
            mSend = FindViewById<Button>(Resource.Id.send);
            mSend.Click += mSendOnClick;
        }

        public void mSendOnClick(Object o, EventArgs e)
        {
            
            if(path_to_image != null)
            {
                Intent resultIntent = new Intent();
                resultIntent.PutExtra("TITLE", mTitle.Text);
                resultIntent.PutExtra("LOCATION", mLocation.Text);
                resultIntent.PutExtra("IMAGE", path_to_image);
                SetResult(Result.Ok, resultIntent);
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Attach an Image", ToastLength.Short).Show();
            }
            
        }

        public void mCameraOnClick(Object o, EventArgs e)
        {

            if (IsThereAnAppToTakePictures())
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);

                App._file = new File(App._dir, String.Format("MobileTest_{0}.jpg", Guid.NewGuid()));
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));

                StartActivityForResult(intent, 0);
            }
        }


        public void mGalleryOnClick(Object o, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);

        }

        public void mLastPicOnClick(Object o, EventArgs e)
        {
            path_to_image = getLastTakenPhoto();
            mImageView.SetImageURI(Android.Net.Uri.Parse(getLastTakenPhoto()));
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
            using (ICursor cursor = ManagedQuery(MediaStore.Images.Media.ExternalContentUri, projection, null, null, MediaStore.Images.ImageColumns.DateTaken + " DESC"))
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

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "MobileTest");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private string GetPathToImage(Android.Net.Uri uri)
        {
            string path = null;
            // The projection contains the columns we want to return in our query.
            string[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = ManagedQuery(uri, projection, null, null, null))
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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                mImageView.SetImageURI(uri);

                path_to_image = GetPathToImage(uri);
            }
            else if ((requestCode == 0))
            {
                base.OnActivityResult(requestCode, resultCode, data);

                // Make it available in the gallery

                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);

                path_to_image = GetPathToImage(contentUri);

                int height = Resources.DisplayMetrics.HeightPixels;
                int width = mImageView.Height;
                App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
                if (App.bitmap != null)
                {
                    mImageView.SetImageBitmap(App.bitmap);
                    App.bitmap = null;
                }

                // Dispose of the Java side bitmap.
                GC.Collect();

            }
        }


        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }


    }
}