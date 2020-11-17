using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter;
using StudentTestingApp.View;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Common;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StudentTestingApp
{
    /// <summary>
    /// Класс, определяющий точку входа в приложение
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Запуск приложения: внедрение зависимостей, показ страницы предварительной настройки приложения
        /// </summary>
        public App()
        {
            InitializeComponent();
            new ApplicationController()
                .RegisterSingleton<CloudStorage>()
                .RegisterSingleton<DbInfo>()
                .RegisterSingleton<TestsLoader>()
                .RegisterSingleton<DbHelper>()
                .RegisterSingleton<DnevnikApiAuthentificator>()
                .Register<ReadOnlyRepository<Subject>>()
                .Register<ReadOnlyRepository<Test>>()
                .Register<ReadOnlyRepository<Question>>()
                .Register<ReadOnlyRepository<Answer>>()
                .Register<Repository<TestResult>>()
                .RegisterSingleton<IMessageDialog, MessageDialog>()
                .RegisterSingleton<IWaitingAnimation, WaitingAnimation>()
                .Register<IPreloadView, PreloadPage>()
                .Register<ISubjectsView, SubjectsPage>()
                .Register<ITestsView, TestsPage>()
                .Register<ITestStartView, TestStartPage>()
                .Register<ITestNavigationView, TestNavigationPage>()
                .Register<IQuestionView, QuestionPage>()
                .Register<ITestResultView, TestResultPage>()
                .Register<ITestResultsView, TestResultsPage>()
                .Register<ISettingsView, SettingsPage>()
                .Register<IMainView, MainPage>()
                .CreatePresenter<PreloadPresenter>()
                .Run();
        }

        public static object Navigation { get; internal set; }
    }
}