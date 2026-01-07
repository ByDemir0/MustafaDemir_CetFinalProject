using Microsoft.Extensions.Logging;
using Cet301FinalProject.Data;

namespace Cet301FinalProject;

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

        // --- İŞTE O SİHİRLİ KOD (TAM OLARAK BURAYA) ---
        SQLitePCL.Batteries_V2.Init();
        // ---------------------------------------------

        return builder.Build();
    }
}