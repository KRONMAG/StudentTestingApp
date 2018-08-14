using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter
    {
        public TestNavigationPresenter(IParentView parentView, ITestNavigationView testNavigationView, Test test)
        {
            if (test.Duration != null) testNavigationView.ShowWithTimer(parentView, test);
            else testNavigationView.Show(parentView);
            new Task(() =>
            {
                var questions = DB.Instance.GetQuestions(test);
                foreach (var question in questions)
                {
                    var questionView = App.Container.Resolve<IQuestionView>();
                    testNavigationView.QuestionViews.Add(questionView);
                    new QuestionPresenter(questionView, question);
                }
            }).Start();
        }
    }
}