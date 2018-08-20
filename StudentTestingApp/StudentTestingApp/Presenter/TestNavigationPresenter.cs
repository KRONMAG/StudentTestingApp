using System;
using System.Collections.ObjectModel;
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
                var questionViews = new Collection<IQuestionView>();
                foreach (var question in questions)
                {
                    var questionView = App.Container.Resolve<IQuestionView>();
                    new QuestionPresenter(questionView, question);
                    questionViews.Add(questionView);
                }
                testNavigationView.SetQuestionViews(questionViews);
            }).Start();
        }
    }
}