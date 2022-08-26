using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace HeartsCounter;

[Activity(Theme = "@style/Maui.SplashTheme",
          MainLauncher = true, 
          ScreenOrientation = ScreenOrientation.Portrait, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | 
                           ConfigChanges.UiMode | ConfigChanges.ScreenLayout | 
                           ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static Context AndroidContext { get; private set; }
    public static MainActivity Current { get; private set; }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        AndroidContext = ApplicationContext;
        Current = this;
    }
}
