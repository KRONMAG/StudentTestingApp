using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter;
using Unity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StudentTestingApp
{
    public partial class App : Application
    {
        public static UnityContainer Container { get; private set; }

        public App()
        {
            InitializeComponent();
            Container = new UnityContainer();
            Container.RegisterType<IPreloadView, PreloadPage>();
            Container.RegisterType<ISubjectListView, SubjectListPage>();
            Container.RegisterType<ITestListView, TestListPage>();
            Container.RegisterType<ITestStartView, TestStartPage>();
            new PreloadViewPresenter(Container.Resolve<IPreloadView>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}