using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Java.Lang;

namespace SpellingBlocks
{
    [Activity(Label = "SpellingBlocks"
        , MainLauncher = true
        , Icon = "@drawable/Icon" 
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        Game1 g;

        protected override void OnCreate(Bundle bundle)
        {
            SetUi();
            base.OnCreate(bundle);
            Thread.Sleep(1000);
            g = new Game1();
            SetContentView((View)g.Services.GetService(typeof(View)));

            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            g.Run();
        }

        public void SetUi()
        {
            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetUi();
        }
    }
}

