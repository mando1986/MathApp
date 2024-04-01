using Microsoft.Extensions.Logging;
using MathApp.Services;
using MathApp.View;
namespace MathApp;

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
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MathAppService>();
        builder.Services.AddSingleton<ComputationViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<QuestionViewModel>();
        builder.Services.AddTransient<QuestionPage>();

        return builder.Build();
    }
}

