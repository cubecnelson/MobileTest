using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Collections.Generic;
using System;
using Android.Content;
using Android.Provider;
using Java.IO;
using Android.Graphics;
using Core;

namespace MobileTest.Droid
{
	[Activity (Label = "MobileTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
		private List<Meal> mItems;
		private ListView mListView;
        private TextView add;
        public static readonly int PickMealId = 2000;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

            ActionBar.SetCustomView(Resource.Layout.ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);


            add = FindViewById<TextView>(Resource.Id.add);

            add.Click += (object sender, EventArgs e) => {
                
                StartActivityForResult(typeof(CreateMealActivity), PickMealId);
                
            };

			mListView = FindViewById<ListView> (Resource.Id.listView1);

			mItems = new List<Meal>();
            

			ListViewAdapter adapter = new ListViewAdapter (this, mItems);

			mListView.Adapter = adapter;
		}

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickMealId) && (resultCode == Result.Ok) && (data != null))
            {
                Meal meal = new Meal();
                meal.Title = data.GetStringExtra("TITLE");
                meal.Location = data.GetStringExtra("LOCATION");

                var image = BitmapFactory.DecodeFile(data.GetStringExtra("IMAGE"));
                var ms = new System.IO.MemoryStream();

                image.Compress(Bitmap.CompressFormat.Jpeg, 0, ms);
                meal.ImageSource = ms.ToArray();


                Domain domain = new Domain();
                
                mItems.Add(await domain.UpdateItem(meal));
                
            }

            ListViewAdapter adapter = new ListViewAdapter(this, mItems);

            mListView.Adapter = adapter;
        }


    }
}


