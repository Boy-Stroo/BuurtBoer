using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Mobile_App
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        // Need to change the StatusbarColor to match the SplashTheme
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Set it to the Primary Color which is 00A89B in this case
        }
    }
}