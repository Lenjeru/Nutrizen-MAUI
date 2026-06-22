using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using TiendaCursos.Movil.Auth;
using TiendaCursos.Movil.Repositorios;


namespace TiendaCursos.Movil
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

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7110/")
            });
            builder.Services.AddScoped<IRepositorio, Repositorio>();
            builder.Services.AddSweetAlert2();

            builder.Services.AddAuthorizationCore();
            //builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();

            builder.Services.AddScoped<AuthenticationProviderJWT>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x =>
x.GetRequiredService<AuthenticationProviderJWT>());
            builder.Services.AddScoped<ILoginServicio, AuthenticationProviderJWT>(x =>
x.GetRequiredService<AuthenticationProviderJWT>());

              
            return builder.Build();
        }
    }
}
