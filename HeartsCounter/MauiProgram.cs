using CommunityToolkit.Maui;
using HeartsCounter.Pages.CurrentGame;
using HeartsCounter.Pages.New_Game;
using HeartsCounter.Services.Implementations.Database;
using HeartsCounter.Services.Implementations.Games;
using HeartsCounter.Services.Interfaces.Database;
using HeartsCounter.Services.Interfaces.Games;
using HeartsCounter.ViewModels;
using HeartsCounter.ViewModels.CurrentGame;
using HeartsCounter.ViewModels.NewGame;
using HeartsCounter.Views.BottomSheetsViews;

namespace HeartsCounter;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("arista_pro_bold.ttf", "AristaProBold");
                fonts.AddFont("arista_pro_light.ttf", "AristaProLight");
            });

		RegisterViewModels(builder);
		RegisterPages(builder);
		RegisterServices(builder);

        return builder.Build();
	}

    public static MauiApp CreateMauiApp(MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("arista_pro_bold.ttf", "AristaProBold");
                fonts.AddFont("arista_pro_light.ttf", "AristaProLight");
            });

        RegisterViewModels(builder);
        RegisterPages(builder);
        RegisterServices(builder);

        return builder.Build();
    }

    private static void RegisterViewModels(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<NewGameViewModel>();
        builder.Services.AddTransient<GameViewModel>();
    }

	private static void RegisterPages(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<NewGamePage>();
        builder.Services.AddTransient<GamePage>();
        builder.Services.AddTransient<AddRoundBottomSheetView>();
    }

	private static void RegisterServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IDatabaseManagerService, DatabaseManagerService>();
        builder.Services.AddSingleton<IGameDbManagerService, GameDbManagerService>();
    }
}
