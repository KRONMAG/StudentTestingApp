using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter : IPresenter
    {
        private ITestNavigationView testNavigationView;
        private Test test;

        public TestNavigationPresenter(ITestNavigationView testNavigationView, Test test)
        {
            this.testNavigationView = testNavigationView;
            this.test = test;
        }

        public void Run()
        {
            if (test.Duration != null)
                testNavigationView.ShowWithTimer(test);
            else
                testNavigationView.Show();
            new Task(() =>
            {
                var questions = DB.Instance.GetQuestions(test);
                var questionViews = new Collection<IQuestionView>();
                foreach (var question in questions)
                {
                    var questionView = App.Container.Resolve<IQuestionView>();
                    new QuestionPresenter(questionView, question).Run();
                    questionViews.Add(questionView);
                }
                testNavigationView.SetQuestionViews(questionViews);
            }).Start();
        }
    }
}