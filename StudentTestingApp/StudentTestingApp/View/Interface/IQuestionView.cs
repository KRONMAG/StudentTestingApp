using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface IQuestionView : IView
    {
        void SetQuestion(Question question);
        void AddAnswer(Answer answer);
        IEnumerable<Answer> SelectedAnswers { get; }
    }
}