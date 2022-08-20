using Prism;
using Prism.Ioc;
using Prism.Unity;
using TestFiboTechnologies.Services;
using TestFiboTechnologies.View;
using TestFiboTechnologies.ViewModels;
using Xamarin.Forms;

namespace TestFiboTechnologies
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SingUpPage, SingUpPageViewModel>();
            containerRegistry.Register<IDbService, DbService>();
        }

       
    }
}
