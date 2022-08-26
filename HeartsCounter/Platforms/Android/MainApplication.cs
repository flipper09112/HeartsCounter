using Android.App;
using Android.Runtime;
using HeartsCounter.Services.Interfaces.CrossPlatform;
using HeartsCounter.Services.Interfaces.Database;

namespace HeartsCounter;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();

        builder.Services.AddSingleton<IBottomSheetDialogService, BottomSheetDialogService>();

        return MauiProgram.CreateMauiApp(builder);
    }
}
