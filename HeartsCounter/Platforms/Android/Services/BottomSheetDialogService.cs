using Android.App;
using Android.Hardware.Lights;
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
    private BottomSheetDialog _bottomSheetDialog;

    public void HideCurrentBottomSheet()
    {
        _bottomSheetDialog.Hide();
    }

    public void ShowBottomSheet(Page page, IView bottomSheetContent, bool dimDismiss)
    {
        _bottomSheetDialog = new BottomSheetDialog(MainActivity.Current);
        var view = bottomSheetContent.ToContainerView(page.Handler?.MauiContext ?? throw new Exception("MauiContext is null"));
        view.SetBackgroundColor(Colors.White.AsColor());

        _bottomSheetDialog.SetContentView(view);

        BottomSheetBehavior mBehavior = BottomSheetBehavior.From((Android.Views.View)view.Parent);
        mBehavior.PeekHeight = 1900;

        _bottomSheetDialog.Show(); 
    }
}
