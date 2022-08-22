using HeartsCounter.Pages.New_Game;
using HeartsCounter.Services.Implementations.Database;
using HeartsCounter.Services.Implementations.Games;
using HeartsCounter.Services.Interfaces.Database;
using HeartsCounter.Services.Interfaces.Games;
using HeartsCounter.ViewModels;
using HeartsCounter.ViewModels.NewGame;

namespace HeartsCounter;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
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
    }

	private static void RegisterPages(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<NewGamePage>();
    }

	private static void RegisterServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IDatabaseManagerService, DatabaseManagerService>();
        builder.Services.AddSingleton<IGameDbManagerService, GameDbManagerService>();
    }
}
