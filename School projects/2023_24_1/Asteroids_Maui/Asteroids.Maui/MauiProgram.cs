using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Asteroids.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit();

            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
            return builder.Build();
        }
    }
}
