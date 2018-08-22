using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter : IPresenter
    {
        private IParentView parentView;
        private ITestNavigationView testNavigationView;
        private Test test;

        public TestNavigationPresenter(IParentView parentView, ITestNavigationView testNavigationView, Test test)
        {
            this.parentView = parentView;
            this.testNavigationView = testNavigationView;
            this.test = test;
        }

        public void Run()
        {
            testNavigationView.Show(parentView);
            if (test.Duration != null)
                testNavigationView.StartTimer(test);
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