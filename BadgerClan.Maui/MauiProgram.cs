using BadgerClan.Maui.ViewModels;
using BadgerClan.Maui.Views;
using Microsoft.Extensions.Logging;

namespace BadgerClan.Maui
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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<MainPageViewModel>();
            builder.Services.AddSingleton<ClientViewModel>();
            builder.Services.AddSingleton<ClientPage>();
            return builder.Build();
        }
    }
}
