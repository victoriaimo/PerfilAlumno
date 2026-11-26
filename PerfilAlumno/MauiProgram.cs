using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PerfilAlumno.ViewModels;
using PerfilAlumno.Views;
using PerfilAlumno.Repositories;
using PerfilAlumno.Services;

namespace PerfilAlumno
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HttpClient>();

            builder.Services.AddSingleton<IUserProfileRepository>(sp =>
                new UserProfileRepositorySQLite(
                    Path.Combine(FileSystem.AppDataDirectory, "perfilalumno.db3")));

            builder.Services.AddTransient<UserProfileService>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<UsuariosViewModel>();
            builder.Services.AddTransient<UsuariosPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}