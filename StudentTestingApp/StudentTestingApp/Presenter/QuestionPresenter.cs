using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class QuestionPresenter
    {
        public QuestionPresenter(IQuestionView questionView, Question question)
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