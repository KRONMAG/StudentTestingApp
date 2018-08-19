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
                foreach (var answer in answers)
                    questionView.AddAnswer(answer);
            }).Start();
        }
    }
}