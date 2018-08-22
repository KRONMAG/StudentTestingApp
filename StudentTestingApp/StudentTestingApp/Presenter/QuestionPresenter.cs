using System;
using System.Threading.Tasks;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;

namespace StudentTestingApp.Presenter
{
    public class QuestionPresenter : IPresenter
    {
        private IQuestionView questionView;
        private Question question;

        public QuestionPresenter(IQuestionView questionView, Question question)
        {
            this.questionView = questionView;
            this.question = question;
        }

        public void Run()
        {
            questionView.SetQuestion(question);
            new Task(() =>
            {
                var answers = DB.Instance.GetAnswers(question);
                if (answers != null)
                    questionView.SetAnswers(answers);
            }).Start();
        }
    }
}