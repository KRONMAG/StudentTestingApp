using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter
    {
        public TestNavigationPresenter(ITestNavigationView testNavigationView, Test test)
        {
            if (test.Duration != null)
                testNavigationView.ShowWithTimer(test);
            else
                testNavigationView.Show();
            new Task(() =>
            {
                var questions = DB.Instance.GetQuestions(test);
                foreach (var question in questions)
                {
                    var questionView = App.Container.Resolve<IQuestionView>();
                    Device.BeginInvokeOnMainThread(() => testNavigationView.AddQuestionView(questionView));
                    new QuestionPresenter(questionView, question);
                }
            }).Start();
        }
    }
}