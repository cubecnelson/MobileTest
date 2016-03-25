using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using System;
using Core;
using Android.Graphics;

namespace MobileTest.Droid
{
	public class ListViewAdapter : BaseAdapter<Meal>
	{
		private List<Meal> mItems;
		private Context mContext;
        

		public ListViewAdapter (Context context, List<Meal> items)
		{
			mItems = items;
			mContext = context;
           
		}

		public override int Count {
			get {
				return mItems.Count;
			}
		}

        public override Meal this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListItem, null, false);
            }

            TextView title = row.FindViewById<TextView>(Resource.Id.title);
            title.Text = mItems[position].Title;

            TextView location = row.FindViewById<TextView>(Resource.Id.location);
            location.Text = mItems[position].Location;

            TextView created_date = row.FindViewById<TextView>(Resource.Id.created_date);
            created_date.Text = ""+mItems[position].CreatedDate;

            ImageView imageView = row.FindViewById<ImageView>(Resource.Id.image);
            if (mItems[position].ImageSource != null)
            {
                BitmapFactory.Options o = new BitmapFactory.Options();
                o.InSampleSize = 8;
                Bitmap bitmap = BitmapFactory.DecodeByteArray(mItems[position].ImageSource, 0, mItems[position].ImageSource.Length,o);
           
                imageView.SetImageBitmap(bitmap);
               

            }

            return row;
        }
    }
}

