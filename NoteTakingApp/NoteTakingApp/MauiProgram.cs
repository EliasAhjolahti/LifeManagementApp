using Microsoft.Extensions.Logging;
using NoteTakingApp.Pages;
using NoteTakingApp.ViewModels;

namespace NoteTakingApp
{
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
                }); // <- sulje ketju tähän

            // DI-rekisteröinnit tehdään ketjun ULKOPUOLELLA
            builder.Services.AddTransient<NoteViewModel>();
            builder.Services.AddTransient<NotePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
