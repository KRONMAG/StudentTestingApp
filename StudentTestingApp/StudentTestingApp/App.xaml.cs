using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter;
using StudentTestingApp.View;
using StudentTestingApp.View.Interface;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace StudentTestingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApplicationController.Instance.RegisterModel<IDbLoader, DbLoader>();
            ApplicationController.Instance.RegisterModel<IReadOnlyRepository<Subject>, ReadOnlyRepository<Subject>>();
            ApplicationController.Instance.RegisterModel<IReadOnlyRepository<Test>, ReadOnlyRepository<Test>>();
            ApplicationController.Instance.RegisterModel<IReadOnlyRepository<Question>, ReadOnlyRepository<Question>>();
            ApplicationController.Instance.RegisterModel<IReadOnlyRepository<Answer>, ReadOnlyRepository<Answer>>();
            ApplicationController.Instance.RegisterView<IPreloadView, PreloadPage>();
            ApplicationController.Instance.RegisterView<ISubjectListView, SubjectListPage>();
            ApplicationController.Instance.RegisterView<ITestListView, TestListPage>();
            ApplicationController.Instance.RegisterView<ITestStartView, TestStartPage>();
            ApplicationController.Instance.RegisterView<ITestNavigationView, TestNavigationPage>();
            ApplicationController.Instance.RegisterView<IQuestionView, QuestionPage>();
            ApplicationController.Instance.Run<PreloadPresenter>();
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