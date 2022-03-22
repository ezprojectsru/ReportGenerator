using ReportGenerator.ViewModels;


namespace ReportGenerator.Services
{
    /// <summary>
    /// Класс локатора ViewModel
    /// </summary>
    class ViewModelLocator
    {        
        public AuthorizationViewModel AuthorizationViewModel => InversionOfControl.Resolve<AuthorizationViewModel>();
        public SettingsWindowViewModel SettingsWindowViewModel => InversionOfControl.Resolve<SettingsWindowViewModel>();       
        public MainWindowViewModel MainWindowViewModel => InversionOfControl.Resolve<MainWindowViewModel>();
        public PlanWindowViewModel PlanWindowViewModel => InversionOfControl.Resolve<PlanWindowViewModel>();
        public TaskEditWindowViewModel TaskEditWindowViewModel => InversionOfControl.Resolve<TaskEditWindowViewModel>();
        public PlanEditWindowViewModel PlanEditWindowViewModel => InversionOfControl.Resolve<PlanEditWindowViewModel>();
        public AppManagerPageViewModel AppManagerPageViewModel => InversionOfControl.Resolve<AppManagerPageViewModel>();
        public UserEditWindowViewModel UserEditWindowViewModel => InversionOfControl.Resolve<UserEditWindowViewModel>();
        public DepartamentEditWindowViewModel DepartamentEditWindowViewModel => InversionOfControl.Resolve<DepartamentEditWindowViewModel>();
        public RoleEditWindowViewModel RoleEditWindowViewModel => InversionOfControl.Resolve<RoleEditWindowViewModel>();
    }
}
