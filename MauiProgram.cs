using Microsoft.Extensions.Logging;
using POSApp.Services;

namespace POSApp
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<OrderReceiverService>();
            builder.Services.AddSingleton<POSDatabaseService>();
            builder.Services.AddSingleton<PreparingOrderService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
           
            
#endif

            return builder.Build();
        }
    }
}
