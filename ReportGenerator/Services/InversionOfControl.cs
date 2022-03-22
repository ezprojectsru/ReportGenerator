using Microsoft.Extensions.DependencyInjection;
using ReportGenerator.ViewModels;


namespace ReportGenerator.Services
{
    /// <summary>
    /// Класс IOC
    /// </summary>
    public static class InversionOfControl
    {
        private static readonly ServiceProvider _provider;
        static InversionOfControl()
        {
            var services = new ServiceCollection();

            // Singleton's ViewModels - сохраняет свое состояние         
            services.AddSingleton<AuthorizationViewModel>();            
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<PlanWindowViewModel>();
            services.AddSingleton<AppManagerPageViewModel>();

            // Transient's ViewModels - каждый раз новое состояние
            services.AddTransient<TaskEditWindowViewModel>();
            services.AddTransient<SettingsWindowViewModel>();
            services.AddTransient<PlanEditWindowViewModel>();
            services.AddTransient<UserEditWindowViewModel>();
            services.AddTransient<DepartamentEditWindowViewModel>();
            services.AddTransient<RoleEditWindowViewModel>();

            // Сервисы
            services.AddSingleton<PageNavigationService>();
            _provider = services.BuildServiceProvider();

        }

        public static T Resolve<T>() => _provider.GetRequiredService<T>();
       
    }
}
