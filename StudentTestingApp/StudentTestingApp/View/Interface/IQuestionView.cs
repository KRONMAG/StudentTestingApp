using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.View.Interface
{
    public interface IQuestionView : IView
    {
        void SetQuestion(Question question);
        void SetAnswers(IEnumerable<Answer> answers);
        IEnumerable<Answer> SelectedAnswers { get; }
    }
}