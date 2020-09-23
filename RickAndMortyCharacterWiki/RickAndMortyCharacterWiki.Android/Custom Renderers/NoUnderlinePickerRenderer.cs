using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RickAndMortyCharacterWiki.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(NoUnderlinePickerRenderer))]
namespace RickAndMortyCharacterWiki.Droid.Custom_Renderers
{
    public class NoUnderlinePickerRenderer : PickerRenderer
    {
        private Context context;
        public NoUnderlinePickerRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null) return;

            GradientDrawable gd = new GradientDrawable();
            gd.SetStroke(0, Android.Graphics.Color.LightGray);
            Control.SetBackground(gd);
            Control.Typeface = Typeface.CreateFromAsset(context.Assets, "fonts/Calligraphr-Regular.ttf");
            Control.TextSize = 14;
        }
    }
}