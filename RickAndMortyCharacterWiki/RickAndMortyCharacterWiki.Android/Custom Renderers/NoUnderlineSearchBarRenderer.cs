using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using RickAndMortyCharacterWiki.Custom_Renderers;
using RickAndMortyCharacterWiki.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoUnderlineSearchBar), typeof(NoUnderlineSearchBarRenderer))]
namespace RickAndMortyCharacterWiki.Droid.Custom_Renderers
{
    public class NoUnderlineSearchBarRenderer : SearchBarRenderer
    {
        public NoUnderlineSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);
                //set custom black search icon
                var searchView = Control;
                searchView.Iconified = true;
                searchView.SetIconifiedByDefault(false);
                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var icon = searchView.FindViewById(searchIconId);
                Drawable dr = Resources.GetDrawable(Resource.Drawable.search);
                Bitmap bitmap = ((BitmapDrawable)dr).Bitmap;
                Drawable d = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 50, 50, true));

                (icon as ImageView).SetImageDrawable(d);
                //set text color black
              //  searchView.SetQueryHint(Html.FromHtml("<font color = #000000>" +
              //Resources.GetString(Resource.String.SearchString) + "</font>"));
            }
        }
    }
}