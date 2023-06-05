namespace Journal;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Rubik-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("Rubik-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
