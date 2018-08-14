using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface IQuestionView
    {
        Question Question { get; set; }
        ICollection<Answer> Answers { get; }
        IEnumerable<Answer> SelectedAnswers { get; }
    }
}