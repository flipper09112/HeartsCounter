using Android.Widget;
using Google.Android.Material.BottomSheet;
using HeartsCounter.Services.Interfaces.CrossPlatform;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Platform;

namespace HeartsCounter.Services.Interfaces.CrossPlatform;

public class BottomSheetDialogService : IBottomSheetDialogService
{
    public void ShowBottomSheet(Page page, IView bottomSheetContent, bool dimDismiss)
    {
        var bottomSheetDialog = new BottomSheetDialog(MainActivity.Current);
        var view = bottomSheetContent.ToContainerView(page.Handler?.MauiContext ?? throw new Exception("MauiContext is null"));
        view.SetBackgroundColor(Colors.Yellow.AsColor());
        bottomSheetDialog.SetContentView(view);
        bottomSheetDialog.Show(); 
    }
}
